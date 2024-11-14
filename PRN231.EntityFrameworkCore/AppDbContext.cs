using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRN231.Domain.Entities;
using System.Linq.Expressions;

namespace PRN231.EntityFrameworkCore;

public sealed partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        IConfiguration configuration = builder.Build();
        string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        optionsBuilder.UseNpgsql(_connectionString, buidler =>
        {
            buidler.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
    }

    internal DbSet<Anime>? Animes { get; set; }
    internal DbSet<AuditLog>? AuditLogs { get; set; }
    internal DbSet<Genre>? Genres { get; set; }
    internal DbSet<User>? Users { get; set; }
    internal DbSet<UserToken>? UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // Filter Soft Deleted
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetProperty("IsDeleted") != null)
            {
                var parameter = Expression.Parameter(entityType.ClrType, "x");
                var property = Expression.Property(parameter, "IsDeleted");
                var notDeleted = Expression.Equal(property, Expression.Constant(false));

                var lambda = Expression.Lambda(notDeleted, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
