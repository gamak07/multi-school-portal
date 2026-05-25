namespace MultiPortalSchoolSys.Domain.Enums;

public enum UserStatus
{
    PendingActivation = 1,  // Account created, credentials not yet sent
    Active = 2,             // User has logged in and changed password
    Suspended = 3,          // Temporarily blocked by Admin
    Deactivated = 4         // Permanently removed from system
}