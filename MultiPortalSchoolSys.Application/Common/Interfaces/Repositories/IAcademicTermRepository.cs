using MultiPortalSchoolSys.Domain.Entities.Calendar;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IAcademicTermRepository : IRepository<AcademicTerm>
{
    // Query 1: Find the single active term governing the current portal operations
    Task<AcademicTerm?> GetCurrentTermAsync();

    // Query 2: Fetch all terms configured under a single academic session
    Task<IEnumerable<AcademicTerm>> GetByAcademicYearAsync(string academicYear);

    // Query 3: Find a specific term matching a unique year and tier sequence number
    Task<AcademicTerm?> GetByYearAndTermNumberAsync(string academicYear, int termNumber);

    // Query 4: Load an individual term record complete with its full calendar event itinerary
    Task<AcademicTerm?> GetWithEventsByIdAsync(int id);
}