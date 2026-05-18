using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class ParentRepository : Repository<Parent>, IParentRepository
{
    public ParentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Parent?> GetWithChildrenAsync(int parentId)
        => await _context.Parents
            .Include(p => p.Children)
            .FirstOrDefaultAsync(p => p.Id == parentId);

    public async Task<Parent?> GetByUserIdAsync(string userId)
        => await _context.Parents
            .Include(p => p.Children)
            .FirstOrDefaultAsync(p => p.UserId == userId);
}