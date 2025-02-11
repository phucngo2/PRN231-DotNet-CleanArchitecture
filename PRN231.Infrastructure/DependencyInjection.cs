using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRN231.Domain.Interfaces.Cache;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;
using PRN231.Infrastructure.Cache;
using PRN231.Infrastructure.DataAccess;
using PRN231.Infrastructure.Email;
using Scrutor;
using StackExchange.Redis;

namespace PRN231.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessInfrastructure();
        services.AddRepositories();
        services.AddBackgroundService(configuration);
        services.AddCacheService(configuration);
        services.AddMailService(configuration);
        return services;
    }

    public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(
                filter => filter.Where(x => x.Name.EndsWith("Repository")),
                publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(cfg =>
        {
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? string.Empty);
            return multiplexer.GetDatabase();
        });
        services.AddScoped<IRedisService, RedisService>();

        return services;
    }

    public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailSerivce, EmailService>();

        return services;
    }

    public static IServiceCollection AddBackgroundService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(options =>
                options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"))
            )
        );
        services.AddHangfireServer();

        return services;
    }
}
