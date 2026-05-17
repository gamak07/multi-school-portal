using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Teacher;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface ITeacherService
{
    Task<Result<TeacherDto>> GetByIdAsync(int id);
    Task<Result<IEnumerable<TeacherDto>>> GetAllAsync();
    Task<Result<TeacherDto>> CreateAsync(CreateTeacherDto dto);
    Task<Result> UpdateAsync(int id, UpdateTeacherDto dto);
    Task<Result> AssignSubjectAsync(int teacherId, int subjectId);
    Task<Result> RemoveSubjectAsync(int teacherId, int subjectId);
    Task<Result> DeleteAsync(int id);
}