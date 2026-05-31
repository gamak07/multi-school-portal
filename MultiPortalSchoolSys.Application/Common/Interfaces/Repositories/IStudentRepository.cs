using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    // Query 1: Resolve the lightweight profile shell using the security identity mapping
    Task<Student?> GetByUserIdAsync(int userId);

    // Query 2: Alternate business key lookup used to quickly verify school credentials
    Task<Student?> GetByAdmissionNoAsync(string admissionNo);

    // Query 3: Fetch a single student record complete with classroom and guardian profiles pre-joined
    Task<Student?> GetWithDetailsByIdAsync(int id);

    // Query 4: Retrieve the complete student roster list assigned to a specific classroom channel
    Task<IEnumerable<Student>> GetByClassRoomIdAsync(int classRoomId);

    // Query 5: Retrieve all siblings/children associated with a specific parent tracking ID
    Task<IEnumerable<Student>> GetByParentIdAsync(int parentId);
}