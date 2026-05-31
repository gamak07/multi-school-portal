
using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IGradingSettingRepository : IRepository<GradingSetting>
{
    Task<GradingSetting?> GetWithSubjectAsync(int gradingSettingId);
    Task<GradingSetting?> GetBySubjectIdAsync(int subjectId);
}