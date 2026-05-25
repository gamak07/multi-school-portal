namespace MultiPortalSchoolSys.Domain.Enums;

public enum ExamApprovalStatus
{
    Draft = 1,      // Teacher is still working on it — fully editable
    Submitted = 2,  // Submitted to Admin for review — locked from editing
    Approved = 3,   // Admin approved — released to students or sent to print
    Rejected = 4,   // Admin rejected with remarks — returned to Draft for rework
    Archived = 5    // Past exam, no longer active
}