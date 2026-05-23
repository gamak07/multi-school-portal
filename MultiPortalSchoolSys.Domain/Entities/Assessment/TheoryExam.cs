using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class TheoryExam : BaseEntity
{
    // SECURED: Properties are now properly encapsulated with private setters
    public string Title { get; private set; } = string.Empty;

    public int SubjectId { get; private set; }
    public Subject? Subject { get; private set; }

    public int CreatedByTeacherId { get; private set; }
    public Teacher? CreatedByTeacher { get; private set; }

    public string AcademicYear { get; private set; } = string.Empty;
    public int Term { get; private set; } // 1, 2, or 3
    public decimal TotalMarks { get; private set; }

    public ExamApprovalStatus ApprovalStatus { get; private set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; private set; }
    public int? ApprovedByAdminId { get; private set; } // Consistently matches numeric tracking ids
    public DateTime? ApprovedAt { get; private set; }

    public ICollection<TheoryQuestion> Questions { get; private set; } = [];

    private TheoryExam() { }

    public TheoryExam(string title, int subjectId, int createdByTeacherId, string academicYear, int term, decimal totalMarks)
    {
        UpdateTheoryExam(title, subjectId, createdByTeacherId, academicYear, term, totalMarks);
    }

    public void UpdateTheoryExam(string title, int subjectId, int createdByTeacherId, string academicYear, int term, decimal totalMarks)
    {
        // 1. Core Workflow Security Guards
        if (ApprovalStatus == ExamApprovalStatus.Approved)
            throw new InvalidOperationException("Cannot update an approved theory exam. Please contact the administrator.");
        if (ApprovalStatus == ExamApprovalStatus.Submitted)
            throw new InvalidOperationException("Cannot modify an exam that is currently pending administrative review.");

        // 2. Structural Parameter Input Guards
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (createdByTeacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(createdByTeacherId));
        if (string.IsNullOrWhiteSpace(academicYear)) throw new ArgumentException("Academic year cannot be empty.", nameof(academicYear));
        if (term < 1 || term > 3) throw new ArgumentException("Term must be between 1 and 3.", nameof(term));
        if (totalMarks <= 0) throw new ArgumentException("Total marks must be greater than zero.", nameof(totalMarks));

        // 3. State Machine Transitions
        if (ApprovalStatus == ExamApprovalStatus.Rejected)
        {
            ApprovalStatus = ExamApprovalStatus.Draft; // Drops back to draft safely upon valid rework
            ApprovalRemarks = null;
            ApprovedByAdminId = null;
            ApprovedAt = null;
        }

        Title = title.Trim();
        SubjectId = subjectId;
        CreatedByTeacherId = createdByTeacherId;
        AcademicYear = academicYear.Trim();
        Term = term;
        TotalMarks = totalMarks;
    }

    // FIXED: Parameter types aligned to int and remarks made truly optional for approvals
    public void ApproveExam(int adminId, string? remarks = null)
    {
        if (ApprovalStatus == ExamApprovalStatus.Approved)
            throw new InvalidOperationException("Exam is already approved.");
        
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        ApprovalStatus = ExamApprovalStatus.Approved;
        ApprovedByAdminId = adminId;
        ApprovedAt = DateTime.UtcNow;
        ApprovalRemarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
    }

    // FIXED: Remarks strictly enforced on administrative rejections
    public void RejectExam(int adminId, string remarks)
    {
        if (ApprovalStatus == ExamApprovalStatus.Rejected)
            throw new InvalidOperationException("Exam is already rejected.");
        
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));
        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("Rejection remarks are completely mandatory.", nameof(remarks));

        ApprovalStatus = ExamApprovalStatus.Rejected;
        ApprovedByAdminId = adminId;
        ApprovedAt = DateTime.UtcNow;
        ApprovalRemarks = remarks.Trim();
    }
}