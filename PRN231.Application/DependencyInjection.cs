using Microsoft.Extensions.DependencyInjection;
using PRN231.Application.Services.AnimeServices;
using PRN231.Application.Services.AuthServices;
using PRN231.Application.Services.GenreServices;
using System.Reflection;

namespace PRN231.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IAnimeService, AnimeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenreService, GenreService>();

        return services;
    }
}
