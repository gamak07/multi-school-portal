namespace MultiPortalSchoolSys.Domain.Enums;

public enum LeaveStatus
{
    Pending,    // Submitted, awaiting Admin review
    Approved,   // Admin approved the leave
    Rejected,   // Admin rejected with remarks
    Cancelled   // Teacher cancelled their own request
}