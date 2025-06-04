using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRN231.Domain.Common;

namespace PRN231.EntityFrameworkCore;

public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddDatabase(this IServiceCollection services, IConfiguration configuration, CancellationToken cancellationToken = default)
    {
        // DbContext
        string connectionString = Env.POSTGRES ?? configuration.GetConnectionString("PRN231") ?? string.Empty;
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Run Migration
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        using (var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                /*throw new Exception($"Error while applying db migration.", ex);*/
            }
        }

        // For DbFactory
        services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetRequiredService<AppDbContext>());

        return services;
    }
}
