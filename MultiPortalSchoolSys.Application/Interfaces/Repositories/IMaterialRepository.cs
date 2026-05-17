using MultiPortalSchoolSys.Domain.Entities.Content;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IMaterialRepository : IRepository<Material>
{
    Task<IEnumerable<Material>> GetBySubjectAndClassAsync(int subjectId, int classRoomId);
    Task<IEnumerable<Material>> GetByTeacherAsync(int teacherId);
}