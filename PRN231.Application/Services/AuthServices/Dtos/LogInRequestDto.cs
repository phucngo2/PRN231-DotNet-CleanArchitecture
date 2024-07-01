using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class LogInRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    public string Password { get; set; }
}
