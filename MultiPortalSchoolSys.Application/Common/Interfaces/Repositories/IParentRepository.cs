using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IParentRepository : IRepository<Parent>
{
    // Query 1: Resolve the lightweight parent profile shell using the security identity mapping
    Task<Parent?> GetByUserIdAsync(int userId);

    // Query 2: Fetch the parent record by database ID and eagerly populate their linked child rows
    Task<Parent?> GetWithChildrenByIdAsync(int id);

    // Query 3: Fetch the parent record by identity User ID and eagerly populate their linked child rows
    Task<Parent?> GetWithChildrenByUserIdAsync(int userId);
}