using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class PrintableExam : BaseEntity
{
    private PrintableExam() { }
    public string Title { get; private set; } = string.Empty;
    public int SubjectId { get; private set; }
    public Subject? Subject { get; private set; }
    public int CreatedByTeacherId { get; private set; }
    public Teacher? CreatedByTeacher { get; private set; }
    public int AcademicTermId { get; private set; }
    public AcademicTerm? AcademicTerm { get; private set; }
    public string DocumentUrl { get; private set; } = string.Empty;
    public ExamApprovalStatus ApprovalStatus { get; private set; } = ExamApprovalStatus.Draft;
    public string? ApprovalRemarks { get; private set; }
    public int? ApprovedByAdminId { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

    public PrintableExam(string title, int subjectId, int createdByTeacherId, string documentUrl, int academicTermId)
    {
        if (createdByTeacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(createdByTeacherId));

        CreatedByTeacherId = createdByTeacherId;
        UpdatePrintableExam(title, documentUrl, academicTermId, subjectId);
    }

    public void UpdatePrintableExam(string title, string documentUrl, int academicTermId, int subjectId)
    {
        if (ApprovalStatus == ExamApprovalStatus.Approved)
        {
            throw new InvalidOperationException("Cannot update an approved exam. Please contact the administrator.");
        }

        if (ApprovalStatus == ExamApprovalStatus.Submitted)
        {
            throw new InvalidOperationException("Cannot modify an exam that is currently pending administrative review.");
        }

        if (academicTermId <= 0) throw new ArgumentException("Invalid academic term ID.", nameof(academicTermId));
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(documentUrl)) throw new ArgumentException("Document URL cannot be empty.", nameof(documentUrl));


        if (ApprovalStatus == ExamApprovalStatus.Rejected)
        {
            ApprovalStatus = ExamApprovalStatus.Draft; // Reset to draft for re-approval
            ApprovalRemarks = null;
            ApprovedByAdminId = null;
            ApprovedAt = null;
        }

        Title = title.Trim();
        DocumentUrl = documentUrl.Trim();
        SubjectId = subjectId;
        AcademicTermId = academicTermId;
    }

    public void ApproveExam(int adminId, string? remarks = null)
    {
        EnsurePendingReview();

        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        ApprovalStatus = ExamApprovalStatus.Approved;
        ApprovedByAdminId = adminId;
        ApprovalRemarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
        ApprovedAt = DateTime.UtcNow;
    }

    public void RejectExam(int adminId, string remarks)
    {
        EnsurePendingReview();

        if (adminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(adminId));

        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("Remarks cannot be empty when rejecting an exam.", nameof(remarks));

        ApprovalStatus = ExamApprovalStatus.Rejected;
        ApprovedByAdminId = adminId;
        ApprovalRemarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
        ApprovedAt = DateTime.UtcNow;
    }

    public void SubmitForReview()
    {
        if (ApprovalStatus != ExamApprovalStatus.Draft)
            throw new InvalidOperationException(
                "Only draft exams can be submitted for review.");
        ApprovalStatus = ExamApprovalStatus.Submitted;
    }

    private void EnsurePendingReview()
    {
        if (ApprovalStatus != ExamApprovalStatus.Submitted)
            throw new InvalidOperationException($"Action denied. This exam cannot be processed because its current status is {ApprovalStatus} instead of Submitted.");
    }
}



