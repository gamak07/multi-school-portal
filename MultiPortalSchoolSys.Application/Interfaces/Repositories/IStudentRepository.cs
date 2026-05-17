using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetByClassAsync(int classRoomId);
    Task<Student?> GetWithParentAsync(int studentId);
    Task<Student?> GetWithDetailsAsync(int studentId);
    Task<IEnumerable<Student>> SearchByNameAsync(string name);
    Task<Student?> GetByAdmissionNoAsync(string admissionNo);
    Task<Student?> GetByUserIdAsync(string userId);
}