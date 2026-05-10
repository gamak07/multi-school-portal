using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IPayrollRepository : IRepository<Payroll>
{
    Task<IEnumerable<Payroll>> GetByTeacherAndMonthAsync(int teacherId, int month, int year);
    Task<IEnumerable<Payroll>> GetMonthlyRunSummaryAsync(int month, int year);
}