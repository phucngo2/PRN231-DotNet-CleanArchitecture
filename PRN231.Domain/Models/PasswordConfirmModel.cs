using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Models;

public abstract class PasswordConfirmModel : PasswordModel
{
    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
}
