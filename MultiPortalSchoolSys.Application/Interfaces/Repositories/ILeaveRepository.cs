using MultiPortalSchoolSys.Domain.Entities.HR;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface ILeaveRepository : IRepository<LeaveRequest>
{
    Task<IEnumerable<LeaveRequest>> GetByTeacherAsync(int teacherId);
    Task<IEnumerable<LeaveRequest>> GetByStatusAsync(LeaveStatus status);
    Task<IEnumerable<LeaveRequest>> GetPendingAsync();
}