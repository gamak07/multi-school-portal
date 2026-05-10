using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IAttendanceRepository : IRepository<StudentAttendance>
{
    Task<IEnumerable<StudentAttendance>> GetByClassAndDateAsync(int classRoomId, DateOnly date);
    Task<IEnumerable<StudentAttendance>> GetStudentTermSummaryAsync(int studentId, int termId);
    Task<IEnumerable<StudentAttendance>> GetAbsenteesAsync(int classRoomId, DateOnly date);
}