using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.HR;

public class Sanction : BaseEntity
{
    // Covers both Staff and Students — linked to ApplicationUser directly
    [Required]
    public string IssuedToUserId { get; set; } = string.Empty;
    [ForeignKey("IssuedToUserId")]
    // public ApplicationUser? IssuedToUser { get; set; }

    public SanctionType SanctionType { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string IssuedByAdminId { get; set; } = string.Empty;
    [ForeignKey("IssuedByAdminId")]
    // public ApplicationUser? IssuedByAdmin { get; set; }

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    // Null = indefinite (e.g., Expulsion)
    public DateTime? ExpiresAt { get; set; }

    public bool IsResolved { get; set; } = false;
}
