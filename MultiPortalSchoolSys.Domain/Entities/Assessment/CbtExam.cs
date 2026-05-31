using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class CbtExam : BaseEntity
{
    private CbtExam() { } 
    public string Title { get; private set; } = string.Empty;

    public int SubjectId { get; private set; }
    // [ForeignKey("SubjectId")]
    public Subject? Subject { get; private set; }

    // Teacher who created this exam
    public int CreatedByTeacherId { get; private set; }
    // [ForeignKey("CreatedByTeacherId")]
    public Teacher? CreatedByTeacher { get; private set; }

    public int DurationMinutes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    // [Column(TypeName = "decimal(5,2)")]
    public decimal TotalMarks { get; private set; }

    // Approval workflow
    public ExamApprovalStatus ApprovalStatus { get; private set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; private set; }     // Admin feedback on rejection
    public int? ApprovedByAdminId { get; private set; }
    // [ForeignKey("ApprovedByAdminId")]
    // public ApplicationUser? ApprovedByAdmin { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

    public bool IsActive { get; private set; } = false;

    public ICollection<CbtQuestion> Questions { get; private set; } = [];

    public CbtExam(string title, int durationMinutes, DateTime startTime, DateTime endTime, decimal totalMarks, int subjectId, int createdByTeacherId)
    {
        UpdateCbtExam(title, durationMinutes, startTime, endTime, totalMarks, subjectId, createdByTeacherId);

    }

    public void UpdateCbtExam(string title, int durationMinutes, DateTime startTime, DateTime endTime, decimal totalMarks, int subjectId, int createdByTeacherId)
    {
        // Enforce: Cannot edit if it has already been approved
        if (ApprovalStatus == ExamApprovalStatus.Approved)
            throw new InvalidOperationException("Cannot modify an exam once it has been approved by an administrator.");

        if (ApprovalStatus == ExamApprovalStatus.Submitted)
            throw new InvalidOperationException("Cannot modify an exam that is currently under review. Please wait for admin approval or rejection.");

        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Exam title cannot be empty.", nameof(title));
        if (durationMinutes <= 0) throw new ArgumentException("Duration must be a positive integer.", nameof(durationMinutes));
        if (totalMarks <= 0) throw new ArgumentException("Total marks must be a positive value.", nameof(totalMarks));
        if (startTime >= endTime) throw new ArgumentException("Start time must be before end time.");
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (createdByTeacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(createdByTeacherId));

        if (ApprovalStatus == ExamApprovalStatus.Rejected)
        {
            ApprovalStatus = ExamApprovalStatus.Draft;
            ApprovalRemarks = null;
            ApprovedByAdminId = null;
            ApprovedAt = null;
        }

        Title = title;
        DurationMinutes = durationMinutes;
        StartTime = startTime;
        EndTime = endTime;
        TotalMarks = totalMarks;
        SubjectId = subjectId;
        CreatedByTeacherId = createdByTeacherId;

        // Recalculate activity status based on the new times provided
        UpdateIsActiveStatus();
    }

    public void SubmitForReview()
    {
        if (ApprovalStatus != ExamApprovalStatus.Draft)
            throw new InvalidOperationException("Only draft examinations can be submitted for review.");

        ApprovalStatus = ExamApprovalStatus.Submitted; // Safe, intentional state progression
    }

    public void ApproveExam(int adminId, string? remarks = null)
    {
        EnsurePendingReview();
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        ApprovedByAdminId = adminId;
        ApprovalRemarks = remarks;
        ApprovalStatus = ExamApprovalStatus.Approved; // Explicit state change
        ApprovedAt = DateTime.UtcNow;
        ApprovalRemarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();

        UpdateIsActiveStatus();
    }

    public void RejectExam(int adminId, string remarks)
    {
        EnsurePendingReview();
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));
        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("Remarks are mandatory when rejecting an exam.", nameof(remarks));

        ApprovedByAdminId = adminId;
        ApprovalRemarks = remarks;
        ApprovalStatus = ExamApprovalStatus.Rejected;
        ApprovedAt = DateTime.UtcNow;
        ApprovalRemarks = remarks.Trim();

        UpdateIsActiveStatus();
    }

    // Pure dynamic calculation using the current internal state! No parameter needed.
    public void UpdateIsActiveStatus()
    {
        IsActive = ApprovalStatus == ExamApprovalStatus.Approved &&
                   DateTime.UtcNow >= StartTime &&
                   DateTime.UtcNow <= EndTime;
    }

    private void EnsurePendingReview()
    {
        if (ApprovalStatus != ExamApprovalStatus.Submitted)
            throw new InvalidOperationException($"Action denied. This exam cannot be modified because its current status is {ApprovalStatus} instead of Submitted.");
    }
}
