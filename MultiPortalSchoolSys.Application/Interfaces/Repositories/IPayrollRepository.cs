using MultiPortalSchoolSys.Domain.Entities.Finance;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IPayrollRepository : IRepository<Payroll>
{
    Task<IEnumerable<Payroll>> GetByTeacherAndMonthAsync(int teacherId, int month, int year);
    Task<IEnumerable<Payroll>> GetMonthlyRunSummaryAsync(int month, int year);
}