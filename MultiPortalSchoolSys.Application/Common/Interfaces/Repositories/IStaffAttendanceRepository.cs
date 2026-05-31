using MultiPortalSchoolSys.Domain.Entities.HR;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStaffAttendanceRepository : IRepository<StaffAttendance>
{
    // Query 1: Find a specific single teacher's attendance status for a precise date
    Task<StaffAttendance?> GetByTeacherAndDateAsync(int teacherId, DateTime date);

    // Query 2: Fetch a historical calendar log for an individual teacher across a date window
    Task<IEnumerable<StaffAttendance>> GetHistoryByTeacherIdAsync(int teacherId, DateTime startDate, DateTime endDate);

    // Query 3: Pull down the full roster attendance ledger for a specific calendar day with employee names pre-joined
    Task<IEnumerable<StaffAttendance>> GetWithTeacherByDateAsync(DateTime date);
}