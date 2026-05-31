using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ISchoolEventRepository : IRepository<SchoolEvent>
{
    // Query 1: Fetch events happening within a specific calendar window
    Task<IEnumerable<SchoolEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    // Query 2: Fetch all events linked to a specific academic term anchor
    Task<IEnumerable<SchoolEvent>> GetByAcademicTermIdAsync(int academicTermId);

    // Query 3: Filter events by their classification type (e.g., Holidays, Exams)
    Task<IEnumerable<SchoolEvent>> GetByEventTypeAsync(EventType eventType);
}