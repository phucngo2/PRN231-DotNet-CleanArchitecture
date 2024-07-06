using PRN231.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class Anime : AuditableEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int Year { get; set; } = DateTime.Now.Year;
    public bool IsAired {  get; set; } = false;

    public ICollection<Genre> Genres { get; set; }
}
