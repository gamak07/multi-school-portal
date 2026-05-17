namespace MultiPortalSchoolSys.Domain.Enums;

public enum UserStatus
{
    PendingActivation,  // Account created, credentials not yet sent
    Active,             // User has logged in and changed password
    Suspended,          // Temporarily blocked by Admin
    Deactivated         // Permanently removed from system
}