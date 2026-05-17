namespace MultiPortalSchoolSys.Domain.Enums;

public enum ExamApprovalStatus
{
    Draft,      // Teacher is still working on it — fully editable
    Submitted,  // Submitted to Admin for review — locked from editing
    Approved,   // Admin approved — released to students or sent to print
    Rejected,   // Admin rejected with remarks — returned to Draft for rework
    Archived    // Past exam, no longer active
}