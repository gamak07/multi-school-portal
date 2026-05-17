using Microsoft.AspNetCore.Identity;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultiPortalSchoolSys.Infrastructure.Identity;

/// <summary>
/// ApplicationUser lives in Infrastructure because it extends IdentityUser
/// which is an ASP.NET Core framework type. Domain stays framework-free.
/// All UserId foreign keys in Domain entities point to this class's Id,
/// with relationships configured in ApplicationDbContext.
/// </summary>
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public UserStatus Status { get; set; } = UserStatus.PendingActivation;
    public bool IsFirstLogin { get; set; } = true;
    public bool IsActive { get; set; } = true;

    // Audit trail — which Admin created this account
    public string? CreatedByAdminId { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}