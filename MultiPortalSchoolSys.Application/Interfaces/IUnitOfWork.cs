using MultiPortalSchoolSys.Application.Interfaces.Repositories;

namespace MultiPortalSchoolSys.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // People
    IStudentRepository    Students    { get; }
    ITeacherRepository    Teachers    { get; }
    IParentRepository     Parents     { get; }

    // Academic
    IResultRepository     Results     { get; }
    IAttendanceRepository Attendances { get; }

    // Assessment
    IExamRepository       Exams       { get; }

    // Finance
    IFeeRepository        Fees        { get; }
    IPayrollRepository    Payrolls    { get; }

    // HR
    ILeaveRepository      Leaves      { get; }
    ISanctionRepository   Sanctions   { get; }

    // Content
    IMaterialRepository   Materials   { get; }

    // Calendar
    ICalendarRepository   Calendar    { get; }

    Task<int> SaveChangesAsync();
}