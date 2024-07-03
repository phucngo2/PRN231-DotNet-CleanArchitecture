using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PRN231.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static async Task<IServiceCollection> AddDatabase(this IServiceCollection services, IConfiguration configuration, CancellationToken cancellationToken = default)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Run Migration
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        using (var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                /*throw new Exception($"Error while applying db migration.", ex);*/
            }
        }

        // DbFactory
        services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetRequiredService<AppDbContext>());

        return services;
    }
}
