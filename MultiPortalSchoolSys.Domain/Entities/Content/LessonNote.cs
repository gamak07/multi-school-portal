using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums; 

namespace MultiPortalSchoolSys.Domain.Entities.Content;

public class LessonNote : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string DocumentUrl { get; private set; } = string.Empty;
    public int SubjectId { get; private set; }
    public Subject? Subject { get; private set; }
    public int TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }
    public int AcademicTermId { get; private set; }
    public AcademicTerm? AcademicTerm { get; private set; }
    public int WeekNumber { get; private set; }
    public LessonNoteStatus ApprovalStatus { get; private set; } = LessonNoteStatus.Draft;
    public string? Remarks { get; private set; }
    public int? ApprovedByAdminId { get; private set; }
    public DateTime? ActionedAt { get; private set; }

    private LessonNote() { }

    public LessonNote(string title, string documentUrl, int subjectId, int teacherId, int academicTermId, int weekNumber)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));

        TeacherId = teacherId;

        UpdateDetails(title, documentUrl, subjectId, academicTermId, weekNumber);
    }

    public void UpdateDetails(string title, string documentUrl, int subjectId, int academicTermId, int weekNumber)
    {
        if (ApprovalStatus == LessonNoteStatus.Approved)
            throw new InvalidOperationException("Cannot modify a lesson note that has already been approved by the principal.");

        if (ApprovalStatus == LessonNoteStatus.Submitted)
            throw new InvalidOperationException("Cannot modify a lesson note that is currently pending review. You can only edit notes that are in Draft status or have been Rejected.");

        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(documentUrl)) throw new ArgumentException("Document URL cannot be empty.", nameof(documentUrl));

        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (academicTermId <= 0) throw new ArgumentException("Invalid academic term ID.", nameof(academicTermId));
        if (weekNumber < 1) throw new ArgumentException("Week number cannot be less than 1.", nameof(weekNumber));
        // If it was rejected previously, editing it resets it back to a draft state for re-review
        if (ApprovalStatus == LessonNoteStatus.Rejected)
        {
            ApprovalStatus = LessonNoteStatus.Draft;
            Remarks = null;
            ApprovedByAdminId = null;
            ActionedAt = null;
        }

        SubjectId = subjectId;
        AcademicTermId = academicTermId;
        WeekNumber = weekNumber;
        Title = title.Trim();
        DocumentUrl = documentUrl.Trim();
    }

    public void SubmitForReview()
    {
        if (ApprovalStatus != LessonNoteStatus.Draft)
            throw new InvalidOperationException("Only draft lesson notes can be submitted for review.");

        ApprovalStatus = LessonNoteStatus.Submitted;
    }

    
    public void Approve(int adminId, string? remarks = null)
    {
        EnsurePendingReview();
        
        if (adminId <= 0) throw new ArgumentException("Invalid Admin ID.", nameof(adminId));

        ApprovalStatus = LessonNoteStatus.Approved;
        ApprovedByAdminId = adminId;
        ActionedAt = DateTime.UtcNow;
        Remarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
    }

    public void Reject(int adminId, string remarks)
    {
        EnsurePendingReview();
       
        
        if (adminId <= 0) throw new ArgumentException("Invalid Admin ID.", nameof(adminId));
        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("You must provide feedback remarks explaining why the note was rejected.", nameof(remarks));

        ApprovalStatus = LessonNoteStatus.Rejected;
        ApprovedByAdminId = adminId;
        ActionedAt = DateTime.UtcNow;
        Remarks = remarks.Trim();
    }

    private void EnsurePendingReview()
    {
        if (ApprovalStatus != LessonNoteStatus.Submitted)
            throw new InvalidOperationException($"Action denied. This note cannot be actioned because its current status is {ApprovalStatus} instead of Submitted.");
    }
}