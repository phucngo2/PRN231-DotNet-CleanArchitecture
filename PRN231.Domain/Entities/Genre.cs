using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class Genre
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public ICollection<Anime> Animes { get; set; }
}
