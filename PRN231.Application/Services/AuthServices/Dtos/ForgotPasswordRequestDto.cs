using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class ForgotPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
