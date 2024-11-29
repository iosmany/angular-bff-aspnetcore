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

app.UseBff();
app.MapBffManagementEndpoints();
app.MapRemoteBffApiEndpoint("/api", "https://localhost:5216")
    .RequireAccessToken();
app.UseSpaYarp();

app.Run();