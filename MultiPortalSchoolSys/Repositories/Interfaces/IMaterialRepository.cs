using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<IEnumerable<Material>> GetBySubjectAndClassAsync(int subjectId, int classRoomId);
    Task<IEnumerable<Material>> GetByTeacherAsync(int teacherId);
}