using PRN231.Application.Services.AnimeServices.Dtos;

namespace PRN231.Application.Services.GenreServices.Dtos;

public class GenreDetailResponseDto
{
    public string Title { get; set; } = string.Empty;

    public ICollection<AnimeResponseDto> Animes { get; set; } = [];
}
