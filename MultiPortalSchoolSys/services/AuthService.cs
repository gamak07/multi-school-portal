using Microsoft.AspNetCore.Identity;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Services.Interfaces;
using MultiPortalSchoolSys.ViewModels;

namespace MultiPortalSchoolSys.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser>   _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
        UserManager<ApplicationUser>   userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager   = userManager;
        _signInManager = signInManager;
    }

    // =========================================================================
    // LOGIN
    // Business rules enforced in order:
    //   1. User must exist
    //   2. Account must be active (IsActive = true)
    //   3. Password must be correct (lockout enabled)
    //   4. If IsFirstLogin = true — flag for forced password change
    //   5. Success — caller redirects to correct portal
    // =========================================================================
    public async Task<(bool Success, string? ErrorMessage, bool RequiresPasswordChange)>
        LoginAsync(LoginViewModel model)
    {
        // Rule 1 — user must exist
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            return (false, "Invalid email or password.", false);

        // Rule 2 — account must be active
        // We return the same generic message as rule 1 to prevent
        // user enumeration (attackers cannot tell which accounts exist).
        if (!user.IsActive)
            return (false, "Invalid email or password.", false);

        // Rule 3 — validate password. lockoutOnFailure: true enforces
        // the Identity lockout policy after consecutive failed attempts.
        var result = await _signInManager.PasswordSignInAsync(
            user,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
            return (false, "This account has been temporarily locked. Please try again later.", false);

        if (!result.Succeeded)
            return (false, "Invalid email or password.", false);

        // Rule 4 — first login requires forced password change
        if (user.IsFirstLogin)
            return (true, null, true);

        // Rule 5 — successful login, proceed to portal
        return (true, null, false);
    }

    // =========================================================================
    // CHANGE PASSWORD (forced first-login flow)
    // Business rules:
    //   1. Locate the user by ID (already authenticated at this point)
    //   2. Validate current password and change to new password
    //   3. Set IsFirstLogin = false so the flag is never triggered again
    //   4. Refresh the auth cookie — ChangePasswordAsync invalidates
    //      the old security stamp, which would log the user out without this
    // =========================================================================
    public async Task<(bool Success, string? ErrorMessage)>
        ChangePasswordAsync(string userId, ChangePasswordViewModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return (false, "User not found.");

        // Rule 2 — change password via Identity
        var result = await _userManager.ChangePasswordAsync(
            user,
            model.CurrentPassword,
            model.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(" ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }

        // Rule 3 — clear the first-login flag permanently
        user.IsFirstLogin = false;
        await _userManager.UpdateAsync(user);

        // Rule 4 — refresh the auth cookie so the user stays logged in
        await _signInManager.RefreshSignInAsync(user);

        return (true, null);
    }

    // =========================================================================
    // LOGOUT
    // =========================================================================
    public async Task LogoutAsync()
        => await _signInManager.SignOutAsync();

    // =========================================================================
    // PORTAL REDIRECT RESOLVER
    // Reads the user's role and returns the correct Area URL.
    // Returns /Home/Index as a safe fallback if no role is matched —
    // this should never happen in a correctly provisioned account
    // but prevents a redirect loop if it does.
    // =========================================================================
    public async Task<string> GetPortalRedirectUrlAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return "/";

        var roles = await _userManager.GetRolesAsync(user);

        return roles.FirstOrDefault() switch
        {
            "Admin"   => "/Admin/Dashboard/Index",
            "Teacher" => "/Teacher/Dashboard/Index",
            "Student" => "/Student/Dashboard/Index",
            "Parent"  => "/Parent/Dashboard/Index",
            _         => "/"
        };
    }
}