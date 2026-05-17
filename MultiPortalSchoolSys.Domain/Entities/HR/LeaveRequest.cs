using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.HR;

public class LeaveRequest : BaseEntity
{
    public int TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; set; }

    [Required]
    [MaxLength(50)]
    public string LeaveType { get; set; } = string.Empty; // "Annual", "Sick", "Maternity"

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required]
    public string Reason { get; set; } = string.Empty;

    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

    public string? ReviewedByAdminId { get; set; }
    [ForeignKey("ReviewedByAdminId")]
    // public ApplicationUser? ReviewedByAdmin { get; set; }

    public string? ReviewRemarks { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
}
