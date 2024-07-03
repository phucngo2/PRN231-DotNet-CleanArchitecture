using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN231.Domain.Entities;

namespace PRN231.EntityFrameworkCore.Configuration;

internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        /*builder
            .HasOne(e => e.User)
            .WithMany(e => e.AuditLogs)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);*/
    }
}
