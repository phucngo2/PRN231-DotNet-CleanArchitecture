using PRN231.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.Domain.Entities;

public class UserToken
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    [Required]
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiredAt { get; set; } = DateTime.Now.AddSeconds(CacheConstants.Tokens.EXPIRY);

    public User? User { get; set; }
}
