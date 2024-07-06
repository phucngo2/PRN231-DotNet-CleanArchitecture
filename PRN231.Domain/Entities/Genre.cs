using PRN231.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class Genre : AuditableEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public ICollection<Anime> Animes { get; set; }
}
