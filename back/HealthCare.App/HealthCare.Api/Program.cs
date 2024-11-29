using HealthCare.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<PatientsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.Authority = "https://localhost:5001";  //we must use idp server url to validate the token
        o.Audience = "healthcareapp";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//var apiGroup = app.MapGroup("/api");

app.MapGet("/api/v1", () => "Hello World WebApi!")
    .AllowAnonymous();

app.MapGet("/api/v1/patients", async (PatientsRepository service) => await service.GetAllAsync())
    .RequireAuthorization();

app.Run();

