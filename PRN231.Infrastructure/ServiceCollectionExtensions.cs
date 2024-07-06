using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRN231.Domain.Interfaces.Cache;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;
using PRN231.Infrastructure.Cache;
using PRN231.Infrastructure.Data;
using PRN231.Infrastructure.Email;
using PRN231.Infrastructure.Repositories;
using StackExchange.Redis;

namespace PRN231.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddBackgroundService(configuration);
        services.AddCacheService(configuration);
        services.AddMailService(configuration);
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IAuditableEntityRepository<>), typeof(AuditableEntityRepository<>));
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();

        return services;
    }

    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(cfg =>
        {
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
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
