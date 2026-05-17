using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Attendance;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IAttendanceService
{
    Task<Result<IEnumerable<AttendanceDto>>> GetByClassAndDateAsync(int classRoomId, DateOnly date);
    Task<Result<IEnumerable<AttendanceDto>>> GetStudentTermSummaryAsync(int studentId, int term);
    Task<Result<IEnumerable<AttendanceDto>>> GetAbsenteesAsync(int classRoomId, DateOnly date);

    /// <summary>
    /// Takes attendance for an entire class in one batch save.
    /// Replaces any existing records for that date.
    /// </summary>
    Task<Result> TakeAttendanceAsync(TakeAttendanceDto dto);
}