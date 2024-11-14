using PRN231.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class SignUpRequestDto : PasswordConfirmModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
