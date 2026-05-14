using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    
    IStudentRepository    Students    { get; }
    IResultRepository     Results     { get; }
    IAttendanceRepository Attendances { get; }
    IExamRepository       Exams       { get; }
    IFeeRepository        Fees        { get; }
    IMaterialRepository   Materials   { get; }
    IPayrollRepository    Payrolls    { get; }

    
    Task<int> SaveChangesAsync();
}