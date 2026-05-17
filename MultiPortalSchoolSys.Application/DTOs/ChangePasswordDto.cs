using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Auth;
public class ChangePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;
    [Required][MinLength(8)]
    public string NewPassword { get; set; } = string.Empty;
    [Required][Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
