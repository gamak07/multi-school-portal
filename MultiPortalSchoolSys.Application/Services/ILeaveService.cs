using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Leave;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface ILeaveService
{
    Task<Result<IEnumerable<LeaveRequestDto>>> GetByTeacherAsync(int teacherId);
    Task<Result<IEnumerable<LeaveRequestDto>>> GetPendingAsync();
    Task<Result<LeaveRequestDto>> SubmitRequestAsync(CreateLeaveRequestDto dto);
    Task<Result> ApproveAsync(int requestId, string adminId, string? remarks = null);
    Task<Result> RejectAsync(int requestId, string adminId, string remarks);
    Task<Result> CancelAsync(int requestId, int teacherId);
}