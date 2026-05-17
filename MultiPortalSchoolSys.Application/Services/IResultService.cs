using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Result;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IResultService
{
    Task<Result<IEnumerable<ResultDto>>> GetByStudentAndTermAsync(int studentId, int term);
    Task<Result<IEnumerable<ResultDto>>> GetClassResultsAsync(int classRoomId, int term);

    /// <summary>
    /// Student and Parent portals MUST only call this method.
    /// Returns only IsPublished = true results.
    /// </summary>
    Task<Result<IEnumerable<ResultDto>>> GetPublishedResultsAsync(int classRoomId, int term);

    Task<Result> EnterResultAsync(EnterResultDto dto);
    Task<Result> UpdateResultAsync(int resultId, EnterResultDto dto);

    /// <summary>
    /// Publishes all results for a classroom and term.
    /// Only Admin can call this — enforced at service level.
    /// </summary>
    Task<Result> PublishResultsAsync(int classRoomId, int term, string adminId);

    Task<Result> DeleteAsync(int resultId);
}