using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.GenreServices.Dtos;

public class GenreUpsertRequestDto
{
    [Required]
    public string Title { get; set; }
}
