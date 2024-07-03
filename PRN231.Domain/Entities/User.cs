using PRN231.Domain.Entities.Base;
using PRN231.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public UserRoles Role { get; set; } = UserRoles.User;
}
