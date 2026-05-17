using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IResultRepository : IRepository<StudentResult>
{
    Task<IEnumerable<StudentResult>> GetByStudentAndTermAsync(int studentId, int term);
    Task<IEnumerable<StudentResult>> GetClassResultsAsync(int classRoomId, int term);
    Task<IEnumerable<StudentResult>> GetPublishedAsync(int classRoomId, int term);
}