using MultiPortalSchoolSys.Domain.Entities.Finance;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IPayrollRepository : IRepository<Payroll>
{
    // Query 1: Verify or fetch a precise payslip cell for a single instructor
    Task<Payroll?> GetByTeacherAndMonthAsync(int teacherId, string month);

    // Query 2: Fetch a historical collection of all payslips assigned to a specific staff member
    Task<IEnumerable<Payroll>> GetByTeacherIdAsync(int teacherId);

    // Query 3: Load the complete payroll grid for a specific month with full employee names joined
    Task<IEnumerable<Payroll>> GetWithTeacherByMonthAsync(string month);

    // Query 4: Isolate paid vs unpaid salary slips within a target month tier
    Task<IEnumerable<Payroll>> GetWithTeacherByMonthAndStatusAsync(string month, bool isPaid);
}