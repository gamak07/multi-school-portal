using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class CalendarRepository : Repository<SchoolEvent>, ICalendarRepository
{
    public CalendarRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<SchoolEvent>> GetByTermAsync(int termId)
        => await _context.SchoolEvents
            .Include(e => e.AcademicTerm)
            .Where(e => e.AcademicTermId == termId)
            .OrderBy(e => e.StartDate)
            .ToListAsync();

    public async Task<IEnumerable<SchoolEvent>> GetUpcomingAsync(int daysAhead = 30)
    {
        var now    = DateTime.UtcNow;
        var cutoff = now.AddDays(daysAhead);
        return await _context.SchoolEvents
            .Where(e => e.StartDate >= now && e.StartDate <= cutoff)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<AcademicTerm?> GetCurrentTermAsync()
        => await _context.AcademicTerms
            .FirstOrDefaultAsync(t => t.IsCurrentTerm);

    public async Task<IEnumerable<AcademicTerm>> GetAllTermsAsync()
        => await _context.AcademicTerms
            .OrderByDescending(t => t.StartDate)
            .ToListAsync();
}