using HealthCare.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers()
//.AddJsonOptions(cfg =>
//{
//    cfg.JsonSerializerOptions.WriteIndented = true;
//    cfg.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    cfg.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//});

builder.Services.AddSingleton<PatientsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.Authority = "https://localhost:5001";
        o.Audience = "healthcareaudapi";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World WebApi!")
    .AllowAnonymous();
app.MapGet("/patients", async (PatientsRepository service) => await service.GetAllAsync())
    .AllowAnonymous();

app.Run();
