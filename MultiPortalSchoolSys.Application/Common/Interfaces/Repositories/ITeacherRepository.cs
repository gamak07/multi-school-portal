using MultiPortalSchoolSys.Domain.Entities.People;


namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ITeacherRepository : IRepository<Teacher>
{
    // Query 1: Resolve the lightweight profile shell using the security identity mapping
    Task<Teacher?> GetByUserIdAsync(int userId);

    // Query 2: Alternate business key lookup used to verify employee employment profiles
    Task<Teacher?> GetByStaffNoAsync(string staffNo);

    // Query 3: Fetch a single instructor by database ID and eagerly populate their assigned subjects
    Task<Teacher?> GetWithSubjectsByIdAsync(int id);

    // Query 4: Fetch the instructor profile by user identity tracking ID and populate their subject tracks
    Task<Teacher?> GetWithSubjectsByUserIdAsync(int userId);

    // Query 5: Retrieve a comprehensive school list of all staff members with their curriculum streams attached
    Task<IEnumerable<Teacher>> GetAllWithSubjectsAsync();
}