using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiPortalSchoolSys.Services.Interfaces;
using MultiPortalSchoolSys.ViewModels;
using System.Security.Claims;

namespace MultiPortalSchoolSys.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    // =========================================================================
    // GET /Account/Login
    // Redirects already-authenticated users straight to their portal.
    // =========================================================================
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var url    = await _authService.GetPortalRedirectUrlAsync(userId);
            return Redirect(url);
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // =========================================================================
    // POST /Account/Login
    // =========================================================================
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var (success, errorMessage, requiresPasswordChange) =
            await _authService.LoginAsync(model);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, errorMessage!);
            return View(model);
        }

        // First-login: force password change before portal access
        if (requiresPasswordChange)
            return RedirectToAction(nameof(ChangePassword));

        // Honour the returnUrl if it is local — prevents open redirect attacks
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        var userId      = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var portalUrl   = await _authService.GetPortalRedirectUrlAsync(userId);
        return Redirect(portalUrl);
    }

    // =========================================================================
    // GET /Account/ChangePassword
    // Only reachable by authenticated users (IsFirstLogin = true flow).
    // =========================================================================
    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
        => View();

    // =========================================================================
    // POST /Account/ChangePassword
    // =========================================================================
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var (success, errorMessage) = await _authService.ChangePasswordAsync(userId, model);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, errorMessage!);
            return View(model);
        }

        // Password changed — redirect to their portal
        var portalUrl = await _authService.GetPortalRedirectUrlAsync(userId);
        return Redirect(portalUrl);
    }

    // =========================================================================
    // POST /Account/Logout
    // POST only — GET logout is a CSRF vulnerability.
    // =========================================================================
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }

    // =========================================================================
    // GET /Account/AccessDenied
    // Called by ASP.NET Core when an authenticated user hits a route
    // their role does not permit.
    // =========================================================================
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
        => View();
}