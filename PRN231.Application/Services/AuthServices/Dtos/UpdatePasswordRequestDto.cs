using PRN231.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN231.Application.Services.AuthServices.Dtos;

public class UpdatePasswordRequestDto : PasswordConfirmModel
{
    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    public string OldPassword { get; set; } = string.Empty;
}
