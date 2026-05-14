using Microsoft.AspNetCore.Mvc;
using MultiPortalSchoolSys.ViewModels;

namespace MultiPortalSchoolSys.Services.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Validates credentials, checks IsActive and IsFirstLogin,
    /// signs the user in, and returns a redirect result to the
    /// correct portal area. Returns null if login fails, along
    /// with an error message the controller passes to ModelState.
    /// </summary>
    Task<(bool Success, string? ErrorMessage, bool RequiresPasswordChange)>
        LoginAsync(LoginViewModel model);

    /// <summary>
    /// Changes the authenticated user's password, sets IsFirstLogin = false,
    /// refreshes the auth cookie, and returns the portal redirect URL.
    /// </summary>
    Task<(bool Success, string? ErrorMessage)>
        ChangePasswordAsync(string userId, ChangePasswordViewModel model);

    /// <summary>
    /// Signs the current user out and clears the auth cookie.
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Resolves the correct Area redirect URL for a given role.
    /// Used by both LoginAsync and ChangePasswordAsync.
    /// </summary>
    Task<string> GetPortalRedirectUrlAsync(string userId);
}