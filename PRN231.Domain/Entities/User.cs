using PRN231.Domain.Entities.Base;
using PRN231.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class User : AuditableEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public UserRoles Role { get; set; } = UserRoles.User;
}
