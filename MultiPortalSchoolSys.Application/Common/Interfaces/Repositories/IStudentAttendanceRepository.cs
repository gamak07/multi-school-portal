
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStudentAttendanceRepository : IRepository<StudentAttendance>
{
    Task<IEnumerable<StudentAttendance>> GetHistoryByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<StudentAttendance>> GetRegisterByClassAndDateAsync(int classId, DateTime date);
    Task<IEnumerable<StudentAttendance>> GetByClassAndStatusAsync(int classId, AttendanceStatus status, DateTime date);
}