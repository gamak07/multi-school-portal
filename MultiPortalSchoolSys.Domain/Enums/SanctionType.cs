namespace MultiPortalSchoolSys.Domain.Enums;

public enum SanctionType
{
    Warning = 1,
    Detention = 2,
    Suspension = 3,
    ProbationaryPeriod = 4,
    Expulsion = 5   // Students only — triggers account deactivation
}