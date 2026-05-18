using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Finance;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class PayrollRepository : Repository<Payroll>, IPayrollRepository
{
    public PayrollRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Payroll>> GetByTeacherAndMonthAsync(int teacherId, int month, int year)
    {
        var monthString = new DateTime(year, month, 1).ToString("MMMM yyyy");
        return await _context.Payrolls
            .Include(p => p.Teacher)
            .Where(p => p.TeacherId == teacherId && p.Month == monthString)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payroll>> GetMonthlyRunSummaryAsync(int month, int year)
    {
        var monthString = new DateTime(year, month, 1).ToString("MMMM yyyy");
        return await _context.Payrolls
            .Include(p => p.Teacher)
            .Where(p => p.Month == monthString)
            .OrderBy(p => p.Teacher!.StaffNo)
            .ToListAsync();
    }
}