using PRN231.Application.Services.GenreServices.Dtos;

namespace PRN231.Application.Services.AnimeServices.Dtos;

public class AnimeDetailResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsAired { get; set; }

    public ICollection<GenreResponseDto> Genres { get; set; } = [];
}
