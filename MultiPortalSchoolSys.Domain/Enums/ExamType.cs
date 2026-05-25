namespace MultiPortalSchoolSys.Domain.Enums;

public enum ExamType
{
    CBT = 1,        // Computer-based, auto-graded
    Theory = 2,     // Written, teacher-graded manually
    Printable = 3   // Teacher uploads doc → Admin approves → physical print
}