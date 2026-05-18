using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Auth;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IAuthService
{
    /// <summary>
    /// Validates credentials, checks IsActive and IsFirstLogin,
    /// signs the user in and returns portal redirect URL.
    /// </summary>
    Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto);

    /// <summary>
    /// Admin provisions a new account for any role.
    /// Creates ApplicationUser + role-specific profile in one atomic transaction.
    /// Sets Status = PendingActivation, IsFirstLogin = true.
    /// </summary>
    Task<Result<ProvisionAccountResponseDto>> ProvisionAccountAsync(ProvisionAccountDto dto);

    /// <summary>
    /// Forced first-login password change.
    /// Sets IsFirstLogin = false and Status = Active on success.
    /// </summary>
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto dto);

    /// <summary>
    /// Suspends an account — sets Status = Suspended, IsActive = false.
    /// Only callable by Admin.
    /// </summary>
    Task<Result> SuspendAccountAsync(string userId, string adminId);

    /// <summary>
    /// Reinstates a suspended account — sets Status = Active, IsActive = true.
    /// </summary>
    Task<Result> ReinstateAccountAsync(string userId, string adminId);

    /// <summary>
    /// Permanently deactivates an account — sets Status = Deactivated.
    /// Cannot be undone without direct DB intervention.
    /// </summary>
    Task<Result> DeactivateAccountAsync(string userId, string adminId);

    /// <summary>
    /// Signs the current user out.
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Resolves the correct Area redirect URL for a given userId based on role.
    /// </summary>
    Task<string> GetPortalRedirectUrlAsync(string userId);
}