using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PRN231.Application.Services.AnimeServices;
using PRN231.Application.Services.AuthServices;
using PRN231.Application.Services.GenreServices;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Domain.Entities;
using System.Reflection;

namespace PRN231.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<IAnimeService, AnimeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenreService, GenreService>();

        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
