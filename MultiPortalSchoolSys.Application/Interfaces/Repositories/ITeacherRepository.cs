using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<IEnumerable<Teacher>> GetAllWithDetailsAsync();
    Task<Teacher?> GetWithDetailsAsync(int teacherId);
    Task<Teacher?> GetByUserIdAsync(string userId);
    Task<Teacher?> GetByStaffNoAsync(string staffNo);
}