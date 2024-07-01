using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class SignUpRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
}
