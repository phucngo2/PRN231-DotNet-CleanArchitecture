using PRN231.Domain.Enums;

namespace PRN231.Domain.Models;

public class JwtModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }
}
