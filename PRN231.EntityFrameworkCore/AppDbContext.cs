using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRN231.Domain.Entities;

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
        string _connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(_connectionString, buidler =>
        {
            buidler.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
    }

    internal DbSet<Anime> Animes { get; set; }
    internal DbSet<AuditLog> AuditLogs { get; set; }
    internal DbSet<Genre> Genres { get; set; }
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
