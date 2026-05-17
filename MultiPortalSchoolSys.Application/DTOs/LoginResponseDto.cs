namespace MultiPortalSchoolSys.Application.DTOs.Auth;
public class LoginResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsFirstLogin { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
}
