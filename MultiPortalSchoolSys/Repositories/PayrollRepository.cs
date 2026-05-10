using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class PayrollRepository : Repository<Payroll>, IPayrollRepository
{
    public PayrollRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Returns payroll entries for a specific teacher in a given month/year.
    /// Payroll.Month is stored as a string (e.g., "October 2025"), so we
    /// construct the expected string from the integer parameters to query against it.
    /// Normally returns one record, but the collection handles edge cases (e.g., corrections).
    /// </summary>
    public async Task<IEnumerable<Payroll>> GetByTeacherAndMonthAsync(int teacherId, int month, int year)
    {
        // Builds "October 2025" from (10, 2025) for matching against the stored string.
        var monthString = new DateTime(year, month, 1).ToString("MMMM yyyy");

        return await _context.Payrolls
            .Include(p => p.Teacher)
                .ThenInclude(t => t!.User)
            .Where(p => p.TeacherId == teacherId && p.Month == monthString)
            .ToListAsync();
    }

    /// <summary>
    /// Returns all payroll entries for every staff member for a given month.
    /// Used by Admin/Finance to review and approve the full monthly payroll run
    /// before triggering payment. Ordered by teacher name for easy review.
    /// </summary>
    public async Task<IEnumerable<Payroll>> GetMonthlyRunSummaryAsync(int month, int year)
    {
        var monthString = new DateTime(year, month, 1).ToString("MMMM yyyy");

        return await _context.Payrolls
            .Include(p => p.Teacher)
                .ThenInclude(t => t!.User)
            .Where(p => p.Month == monthString)
            .OrderBy(p => p.Teacher!.User!.LastName)
            .ToListAsync();
    }
}