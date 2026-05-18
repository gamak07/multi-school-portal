using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Student;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IStudentService
{
    Task<Result<StudentDto>> GetByIdAsync(int id);
    Task<Result<StudentDto>> GetByAdmissionNoAsync(string admissionNo);
    Task<Result<IEnumerable<StudentDto>>> GetByClassAsync(int classRoomId);
    Task<Result<IEnumerable<StudentDto>>> SearchAsync(string name);
    Task<Result<PaginatedList<StudentDto>>> GetAllPaginatedAsync(int page, int pageSize);
    Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto);
    Task<Result> UpdateAsync(int id, UpdateStudentDto dto);
    Task<Result> AssignToClassAsync(int studentId, int classRoomId);
    Task<Result> DeleteAsync(int id);
}