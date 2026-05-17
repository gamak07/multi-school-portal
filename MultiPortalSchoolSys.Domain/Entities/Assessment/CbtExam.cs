using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class CbtExam : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public Subject? Subject { get; set; }

    // Teacher who created this exam
    public int CreatedByTeacherId { get; set; }
    [ForeignKey("CreatedByTeacherId")]
    public Teacher? CreatedByTeacher { get; set; }

    public int DurationMinutes { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal TotalMarks { get; set; }

    // Approval workflow
    public ExamApprovalStatus ApprovalStatus { get; set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; set; }     // Admin feedback on rejection
    public string? ApprovedByAdminId { get; set; }
    [ForeignKey("ApprovedByAdminId")]
    // public ApplicationUser? ApprovedByAdmin { get; set; }
    public DateTime? ApprovedAt { get; set; }

    // Only true when ApprovalStatus = Approved AND within StartTime/EndTime window
    public bool IsActive { get; set; } = false;

    public ICollection<CbtQuestion> Questions { get; set; } = new List<CbtQuestion>();
}
