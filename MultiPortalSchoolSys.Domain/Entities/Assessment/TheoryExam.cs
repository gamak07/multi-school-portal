using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class TheoryExam : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public Subject? Subject { get; set; }

    public int CreatedByTeacherId { get; set; }
    [ForeignKey("CreatedByTeacherId")]
    public Teacher? CreatedByTeacher { get; set; }

    [Required]
    [MaxLength(20)]
    public string AcademicYear { get; set; } = string.Empty;

    public int Term { get; set; } // 1, 2, or 3

    [Column(TypeName = "decimal(5,2)")]
    public decimal TotalMarks { get; set; }

    public ExamApprovalStatus ApprovalStatus { get; set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; set; }
    public string? ApprovedByAdminId { get; set; }
    [ForeignKey("ApprovedByAdminId")]
    // public ApplicationUser? ApprovedByAdmin { get; set; }
    public DateTime? ApprovedAt { get; set; }

    public ICollection<TheoryQuestion> Questions { get; set; } = new List<TheoryQuestion>();
}
