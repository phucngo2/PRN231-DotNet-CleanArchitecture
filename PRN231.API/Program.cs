using Carter;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PRN231.API.Common;
using PRN231.API.Middlewares;
using PRN231.Application;
using PRN231.Application.Helpers;
using PRN231.EntityFrameworkCore;
using PRN231.Infrastructure;
using PRN231.Infrastructure.Hubs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "PRN231");

// Add services to the container.
await builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
// builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        string jwtKey = EnvironmentHelpers.GetJwtKey();
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
        };

        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/hubs")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
            policy.WithOrigins(origins);
            /*.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();*/
        });
});

builder.Services.AddSignalR();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiDoc();

builder.Services.AddCarter();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.UseMiddleware<AuditLogMiddleware>();
// app.UseMiddleware<ExceptionHandlingMiddleware>();

// Available since .NET 8
app.UseExceptionHandler();

app.MapControllers();

//app.MapHealthEndpoints();
app.MapCarter();

app.MapHub<NotificationHub>("/hubs/notification");

app.UseHangfireDashboard("/dashboard");

app.Run();
