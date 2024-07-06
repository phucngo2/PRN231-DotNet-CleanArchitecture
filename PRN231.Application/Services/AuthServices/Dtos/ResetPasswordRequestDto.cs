using PRN231.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class ResetPasswordRequestDto : PasswordConfirmModel
{
    [Required]
    public string Token { get; set; }
}
