using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Payroll;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IPayrollService
{
    Task<Result<IEnumerable<PayrollDto>>> GetMonthlyRunSummaryAsync(int month, int year);
    Task<Result<IEnumerable<PayrollDto>>> GetByTeacherAsync(int teacherId, int month, int year);

    /// <summary>
    /// Generates payroll records for all active teachers for a given month.
    /// NetPay = GrossSalary - Deductions. Computed by service, never trusted from UI.
    /// </summary>
    Task<Result> RunMonthlyPayrollAsync(int month, int year, string adminId);
    Task<Result> MarkAsPaidAsync(int payrollId, string adminId);
}