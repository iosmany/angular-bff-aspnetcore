using Duende.Bff.Yarp;
using HealthCare.Back;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// SpaYarp for proxying requests to the Angular app
//Don't use this in production environment
builder.Services.AddSpaYarp();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .AddServerSideSessions()
    .AddRemoteApis();

builder.Services.AddAuthentication(o =>
{
    //CookieAuthenticationDefaults.AuthenticationScheme: using this scheme for UI(frontend) APP authentication
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(o =>
    {
        o.Cookie.Name = "__HCHost-SPA";  //custom cookie name to avoid conflicts with other cookies
        //samesite to ensure the cookie is not sent in cross-site requests
        o.Cookie.SameSite = SameSiteMode.Strict;

        /*
         Note: if your app is using differents endpoints then configure CORS to ensure only calls from allowed hosts
         */

        o.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(options =>
    {
        //IdentityServer URL (DuendeIdentityProvider)
        options.Authority = IdpConstants.Authority;
        
        options.ClientId = IdpConstants.ClientId;
        options.ClientSecret = IdpConstants.ClientSecret;
        
        options.ResponseType = IdpConstants.ResponseType;
        
        options.Scope.Add(IdpConstants.HealthCareApiScope);
        options.Scope.Add(IdpConstants.OfflineAccess);
        
        options.SaveTokens = true;

    });

var app = builder.Build();

/*
 Together, these lines configure the application to use the BFF middleware and expose the necessary
management endpoints. This setup is essential for implementing the backend-for-frontend pattern, 
which helps in securing and managing interactions between the frontend and backend services in a Blazor application.
 */
app.UseBff();
app.MapBffManagementEndpoints();

//-------------------------------

/*
 The selected code configures the application to proxy requests from the /api endpoint 
to a remote API running at https://localhost:7257. It also ensures that these requests 
require an access token, adding a layer of security to the API access. 
This setup is useful for scenarios where the frontend application needs to interact with a backend API securely.

 */
app.MapRemoteBffApiEndpoint("/api/v1", ApiConfig.ApiEndpoint)
    .RequireAccessToken();
//----------------------------------

app.UseSpaYarp();

app.Run();