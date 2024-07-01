using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN231.Domain.Entities;

namespace PRN231.Infrastructure.Data.Configuration;

internal class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder
            .HasMany(e => e.Genres)
            .WithMany(e => e.Animes);
    }
}
