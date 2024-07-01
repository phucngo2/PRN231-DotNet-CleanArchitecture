using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN231.Domain.Entities;
using PRN231.Domain.Enums;

namespace PRN231.Infrastructure.Data.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(e => e.Email)
            .IsUnique(true);

        builder
            .Property(e => e.Role)
            .HasConversion(
                v => v.ToString(),
                v => UserRoles.FromValue(v)
            );
    }
}
