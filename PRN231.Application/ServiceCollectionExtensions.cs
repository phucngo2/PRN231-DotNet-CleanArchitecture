using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PRN231.Application.Services.AnimeServices;
using PRN231.Application.Services.AuthServices;
using PRN231.Application.Services.GenreServices;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Domain.Entities;
using Scrutor;
using System.Reflection;

namespace PRN231.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddApplicationServicesScrutor();

        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    public static IServiceCollection AddApplicationServicesScrutor(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(ServiceCollectionExtensions).Assembly)
            .AddClasses(
                filter => filter.Where(x => x.Name.EndsWith("Service")),
                publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddApplicationServicesByAssembly(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsInterface &&
                        t.Name.EndsWith("Service") &&
                        t.Namespace?.StartsWith("PRN231.Application") == true)
            .Select(interfaceType => new
            {
                Interface = interfaceType,
                Implementation = assembly.GetTypes()
                    .FirstOrDefault(t =>
                        !t.IsInterface &&
                        interfaceType.IsAssignableFrom(t) &&
                        t.Namespace?.StartsWith("PRN231.Application") == true)
            })
            .Where(x => x.Implementation != null);

        foreach (var serviceType in serviceTypes)
        {
            if (serviceType.Implementation is not null)
            {
                services.AddScoped(serviceType.Interface, serviceType.Implementation);
            }
        }

        return services;
    }

    public static IServiceCollection AddApplicationServicesManually(this IServiceCollection services)
    {
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<IAnimeService, AnimeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenreService, GenreService>();

        return services;
    }
}
