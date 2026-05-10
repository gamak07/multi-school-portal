using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class AttendanceRepository : Repository<StudentAttendance>, IAttendanceRepository
{
    public AttendanceRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Returns every attendance record for a class on a specific date.
    /// DateOnly is converted to a DateTime range for the EF Core query.
    /// Used by teachers to take/view the daily register.
    /// </summary>
    public async Task<IEnumerable<StudentAttendance>> GetByClassAndDateAsync(int classRoomId, DateOnly date)
    {
        var startOfDay = date.ToDateTime(TimeOnly.MinValue);
        var endOfDay = date.ToDateTime(TimeOnly.MaxValue);

        return await _context.StudentAttendances
            .Include(a => a.Student)
                .ThenInclude(s => s!.User)
            .Where(a => a.ClassId == classRoomId &&
                        a.Date >= startOfDay &&
                        a.Date <= endOfDay)
            .OrderBy(a => a.Student!.User!.LastName)
            .ToListAsync();
    }

    /// <summary>
    /// Returns all attendance records for a student across an entire term.
    /// NOTE: StudentAttendance has no Term foreign key. The term is identified
    /// by a date range passed from the Service layer (e.g., Term 1: Sept–Dec).
    /// For now this returns all records for the student; the Service layer
    /// is responsible for filtering by term date boundaries.
    /// </summary>
    public async Task<IEnumerable<StudentAttendance>> GetStudentTermSummaryAsync(int studentId, int termId)
        => await _context.StudentAttendances
            .Where(a => a.StudentId == studentId)
            .OrderBy(a => a.Date)
            .ToListAsync();

    /// <summary>
    /// Returns only the absent students for a class on a given date.
    /// Useful for the admin dashboard absentee report and parent notifications.
    /// </summary>
    public async Task<IEnumerable<StudentAttendance>> GetAbsenteesAsync(int classRoomId, DateOnly date)
    {
        var startOfDay = date.ToDateTime(TimeOnly.MinValue);
        var endOfDay = date.ToDateTime(TimeOnly.MaxValue);

        return await _context.StudentAttendances
            .Include(a => a.Student)
                .ThenInclude(s => s!.User)
            .Where(a => a.ClassId == classRoomId &&
                        a.Date >= startOfDay &&
                        a.Date <= endOfDay &&
                        a.Status == "Absent")
            .OrderBy(a => a.Student!.User!.LastName)
            .ToListAsync();
    }
}