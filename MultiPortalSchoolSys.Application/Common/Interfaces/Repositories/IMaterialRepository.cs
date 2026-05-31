using MultiPortalSchoolSys.Domain.Entities.Content;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IMaterialRepository : IRepository<Material>
{
    // Query 1: Fetch a specific resource element complete with teacher and subject context
    Task<Material?> GetWithDetailsByIdAsync(int id);

    // Query 2: Fetch all downloadable reference materials attached to a specific subject channel
    Task<IEnumerable<Material>> GetWithTeacherBySubjectIdAsync(int subjectId);

    // Query 3: Fetch a history collection of all uploads published by a specific instructor
    Task<IEnumerable<Material>> GetWithSubjectByTeacherIdAsync(int teacherId);
}