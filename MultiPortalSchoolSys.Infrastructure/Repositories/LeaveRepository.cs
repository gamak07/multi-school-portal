using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.HR;
using MultiPortalSchoolSys.Domain.Enums;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class LeaveRepository : Repository<LeaveRequest>, ILeaveRepository
{
    public LeaveRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<LeaveRequest>> GetByTeacherAsync(int teacherId)
        => await _context.LeaveRequests
            .Include(l => l.Teacher)
            .Where(l => l.TeacherId == teacherId)
            .OrderByDescending(l => l.RequestedAt)
            .ToListAsync();

    public async Task<IEnumerable<LeaveRequest>> GetByStatusAsync(LeaveStatus status)
        => await _context.LeaveRequests
            .Include(l => l.Teacher)
            .Where(l => l.Status == status)
            .OrderByDescending(l => l.RequestedAt)
            .ToListAsync();

    public async Task<IEnumerable<LeaveRequest>> GetPendingAsync()
        => await _context.LeaveRequests
            .Include(l => l.Teacher)
            .Where(l => l.Status == LeaveStatus.Pending)
            .OrderBy(l => l.RequestedAt)
            .ToListAsync();
}