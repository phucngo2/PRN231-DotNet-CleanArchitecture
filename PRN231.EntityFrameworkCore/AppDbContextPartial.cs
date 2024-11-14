using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities.Base;

namespace PRN231.EntityFrameworkCore;

public sealed partial class AppDbContext
{
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
        var now = DateTime.Now;

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((AuditableEntity)entity.Entity).CreatedAt = now;
            }
            ((AuditableEntity)entity.Entity).ModifiedAt = now;
        }
    }
}
