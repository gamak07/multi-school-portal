using MultiPortalSchoolSys.Domain.Entities.Calendar;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface ICalendarRepository : IRepository<SchoolEvent>
{
    Task<IEnumerable<SchoolEvent>> GetByTermAsync(int termId);
    Task<IEnumerable<SchoolEvent>> GetUpcomingAsync(int daysAhead = 30);
    Task<AcademicTerm?> GetCurrentTermAsync();
    Task<IEnumerable<AcademicTerm>> GetAllTermsAsync();
}