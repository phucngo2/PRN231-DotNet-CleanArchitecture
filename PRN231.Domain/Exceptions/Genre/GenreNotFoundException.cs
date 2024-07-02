using PRN231.Domain.Exceptions.Common;

namespace PRN231.Domain.Exceptions.Genre;

public sealed class GenreNotFoundException() : NotFoundException("Genre not found!")
{
}
