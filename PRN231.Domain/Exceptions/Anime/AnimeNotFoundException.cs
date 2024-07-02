using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Anime;

public sealed class AnimeNotFoundException() : NotFoundException("Anime not found!")
{
}
