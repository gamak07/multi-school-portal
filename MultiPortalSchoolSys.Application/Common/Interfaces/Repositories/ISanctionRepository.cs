using MultiPortalSchoolSys.Domain.Entities.HR;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ISanctionRepository : IRepository<Sanction>
{
    // Query 1: Fetch an individual disciplinary record with all potential recipient profiles loaded
    Task<Sanction?> GetWithDetailsByIdAsync(int id);

    // Query 2: Retrieve all behavioral tracking files issued against a specific student 
    Task<IEnumerable<Sanction>> GetByStudentIdAsync(int studentId);

    // Query 3: Retrieve all structural HR compliance flags issued against a specific instructor
    Task<IEnumerable<Sanction>> GetByTeacherIdAsync(int teacherId);

    // Query 4: Extract open or resolved disciplinary ledgers with recipient info pre-joined for monitoring
    Task<IEnumerable<Sanction>> GetWithDetailsByResolutionStatusAsync(bool isResolved);
}