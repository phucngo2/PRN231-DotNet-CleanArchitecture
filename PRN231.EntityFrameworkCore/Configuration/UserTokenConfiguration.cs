using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN231.Domain.Entities;

namespace PRN231.EntityFrameworkCore.Configuration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder
            .HasOne(ut => ut.User)
            .WithOne()
            .HasForeignKey<UserToken>(ut => ut.UserId)
            .HasPrincipalKey<User>(u => u.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(u => !u.User.IsDeleted);
    }
}
