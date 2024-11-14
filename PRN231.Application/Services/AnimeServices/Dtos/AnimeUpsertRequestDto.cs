using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AnimeServices.Dtos;

public class AnimeUpsertRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
    public bool IsAired { get; set; } = false;
    public ICollection<int> GenreIds { get; set; } = [];
}
