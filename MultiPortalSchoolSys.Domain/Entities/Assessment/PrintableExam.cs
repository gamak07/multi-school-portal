using MultiPortalSchoolSys.Domain.Common;



using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class PrintableExam : BaseEntity
{

    private PrintableExam() { }
    //     [Required]
    //     [MaxLength(200)]
    public string Title { get; private set; } = string.Empty;

    public int SubjectId { get; private set; }
    // [ForeignKey("SubjectId")]
    public Subject? Subject { get; private set; }

    public int CreatedByTeacherId { get; private set; }
    // [ForeignKey("CreatedByTeacherId")]
    public Teacher? CreatedByTeacher { get; private set; }
    // Path to the uploaded PDF/Word document
    // [Required]
    public string DocumentUrl { get; private set; } = string.Empty;

    // [Required]
    // [MaxLength(20)]
    public string AcademicYear { get; private set; } = string.Empty;

    public int Term { get; private set; }

    public ExamApprovalStatus ApprovalStatus { get; private set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; private set; }
    public int? ApprovedByAdminId { get; private set; }
    // [ForeignKey("ApprovedByAdminId")]
    // public ApplicationUser? ApprovedByAdmin { get; set; }
    public DateTime? ApprovedAt { get; private set; }

    public PrintableExam(string title, int subjectId, int createdByTeacherId, string documentUrl, string academicYear, int term)
    {
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));

        UpdatePrintableExam(title, createdByTeacherId, documentUrl, academicYear, term);
        SubjectId = subjectId;
    }

    public void UpdatePrintableExam(string title, int createdByTeacherId, string documentUrl, string academicYear, int term)
    {
        if (ApprovalStatus == ExamApprovalStatus.Approved)
        {
            throw new InvalidOperationException("Cannot update an approved exam. Please contact the administrator.");
        }
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (createdByTeacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(createdByTeacherId));
        if (string.IsNullOrWhiteSpace(documentUrl)) throw new ArgumentException("Document URL cannot be empty.", nameof(documentUrl));
        if (string.IsNullOrWhiteSpace(academicYear)) throw new ArgumentException("Academic year cannot be empty.", nameof(academicYear));
        if (term <= 0 || term > 3) throw new ArgumentException("Term must be between 1 and 3.", nameof(term));

        if (ApprovalStatus == ExamApprovalStatus.Rejected)
        {
            ApprovalStatus = ExamApprovalStatus.Draft; // Reset to draft for re-approval
            ApprovalRemarks = null;
            ApprovedByAdminId = null;
            ApprovedAt = null;
        }

        Title = title;
        CreatedByTeacherId = createdByTeacherId;
        DocumentUrl = documentUrl;
        AcademicYear = academicYear;
        Term = term;
    }

    public void ApproveExam(int adminId, string remarks)
    {
        if (ApprovalStatus == ExamApprovalStatus.Approved)
        {
            throw new InvalidOperationException("Exam is already approved.");
        }
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        ApprovalStatus = ExamApprovalStatus.Approved;
        ApprovedByAdminId = adminId;
        ApprovalRemarks = remarks;
        ApprovedAt = DateTime.UtcNow;
    }

    public void RejectExam(int adminId, string remarks)
    {
        if (ApprovalStatus == ExamApprovalStatus.Rejected)
    {
        throw new InvalidOperationException("Exam is already rejected.");
    }
        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("Remarks cannot be empty when rejecting an exam.", nameof(remarks));

        ApprovalStatus = ExamApprovalStatus.Rejected;
        ApprovedByAdminId = adminId;
        ApprovalRemarks = remarks;
        ApprovedAt = DateTime.UtcNow;
    }
}



