using MultiPortalSchoolSys.Domain.Entities.HR;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface ISanctionRepository : IRepository<Sanction>
{
    Task<IEnumerable<Sanction>> GetByUserAsync(string userId);
    Task<IEnumerable<Sanction>> GetActiveAsync();
}