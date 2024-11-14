using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class VerifyResetTokenRequestDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
}
