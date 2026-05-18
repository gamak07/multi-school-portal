using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Calendar;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface ICalendarService
{
    Task<Result<AcademicTermDto>> GetCurrentTermAsync();
    Task<Result<IEnumerable<AcademicTermDto>>> GetAllTermsAsync();
    Task<Result<AcademicTermDto>> CreateTermAsync(CreateAcademicTermDto dto);

    /// <summary>
    /// Sets a term as current. Ensures no other term has IsCurrentTerm = true.
    /// Business rule enforced here — not at DB level.
    /// </summary>
    Task<Result> SetCurrentTermAsync(int termId, string adminId);

    Task<Result<IEnumerable<SchoolEventDto>>> GetEventsByTermAsync(int termId);
    Task<Result<IEnumerable<SchoolEventDto>>> GetUpcomingEventsAsync(int daysAhead = 30);
    Task<Result<SchoolEventDto>> CreateEventAsync(CreateSchoolEventDto dto);
    Task<Result> UpdateEventAsync(int eventId, CreateSchoolEventDto dto);
    Task<Result> DeleteEventAsync(int eventId);
}