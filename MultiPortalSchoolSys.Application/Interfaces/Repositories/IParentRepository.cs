using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IParentRepository : IRepository<Parent>
{
    Task<Parent?> GetWithChildrenAsync(int parentId);
    Task<Parent?> GetByUserIdAsync(string userId);
}