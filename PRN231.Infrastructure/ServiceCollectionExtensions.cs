using Microsoft.Extensions.DependencyInjection;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Infrastructure.Data;
using PRN231.Infrastructure.Repositories;

namespace PRN231.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
