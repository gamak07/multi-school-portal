using MultiPortalSchoolSys.Domain.Entities.HR;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ILeaveRequestRepository : IRepository<LeaveRequest>
{
    // Query 1: Fetch an individual leave document with full employee context joined
    Task<LeaveRequest?> GetWithTeacherByIdAsync(int id);

    // Query 2: Fetch a historical submission history ledger for a single instructor
    Task<IEnumerable<LeaveRequest>> GetByTeacherIdAsync(int teacherId);

    // Query 3: Pull the active HR workflow queue matching a target approval milestone status
    Task<IEnumerable<LeaveRequest>> GetWithTeacherByStatusAsync(LeaveStatus status);
}