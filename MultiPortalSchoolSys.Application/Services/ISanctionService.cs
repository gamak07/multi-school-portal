using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Sanction;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface ISanctionService
{
    Task<Result<IEnumerable<SanctionDto>>> GetByUserAsync(string userId);
    Task<Result<IEnumerable<SanctionDto>>> GetActiveAsync();
    Task<Result<SanctionDto>> IssueAsync(CreateSanctionDto dto);

    /// <summary>
    /// Marks a sanction as resolved. For Expulsion, also triggers
    /// account deactivation via AuthService.
    /// </summary>
    Task<Result> ResolveAsync(int sanctionId, string adminId);
}