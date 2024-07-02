using PRN231.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class LogInRequestDto : PasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
