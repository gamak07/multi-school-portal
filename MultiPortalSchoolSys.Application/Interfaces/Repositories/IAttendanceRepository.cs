using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IAttendanceRepository : IRepository<StudentAttendance>
{
    Task<IEnumerable<StudentAttendance>> GetByClassAndDateAsync(int classRoomId, DateOnly date);
    Task<IEnumerable<StudentAttendance>> GetStudentTermSummaryAsync(int studentId, int term);
    Task<IEnumerable<StudentAttendance>> GetAbsenteesAsync(int classRoomId, DateOnly date);
}