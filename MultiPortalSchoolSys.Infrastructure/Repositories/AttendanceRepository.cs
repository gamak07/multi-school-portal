using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class AttendanceRepository : Repository<StudentAttendance>, IAttendanceRepository
{
    public AttendanceRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<StudentAttendance>> GetByClassAndDateAsync(int classRoomId, DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end   = date.ToDateTime(TimeOnly.MaxValue);
        return await _context.StudentAttendances
            .Include(a => a.Student)
            .Where(a => a.ClassId == classRoomId &&
                        a.Date >= start && a.Date <= end)
            .OrderBy(a => a.Student!.AdmissionNo)
            .ToListAsync();
    }

    public async Task<IEnumerable<StudentAttendance>> GetStudentTermSummaryAsync(int studentId, int term)
        => await _context.StudentAttendances
            .Where(a => a.StudentId == studentId)
            .OrderBy(a => a.Date)
            .ToListAsync();

    public async Task<IEnumerable<StudentAttendance>> GetAbsenteesAsync(int classRoomId, DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end   = date.ToDateTime(TimeOnly.MaxValue);
        return await _context.StudentAttendances
            .Include(a => a.Student)
            .Where(a => a.ClassId == classRoomId &&
                        a.Date >= start && a.Date <= end &&
                        a.Status == "Absent")
            .ToListAsync();
    }
}