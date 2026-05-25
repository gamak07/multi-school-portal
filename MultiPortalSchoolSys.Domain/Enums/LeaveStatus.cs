namespace MultiPortalSchoolSys.Domain.Enums;

public enum LeaveStatus
{
    Pending = 1,    // Submitted, awaiting Admin review
    Approved = 2,   // Admin approved the leave
    Rejected = 3,   // Admin rejected with remarks
    Cancelled = 4   // Teacher cancelled their own request
}