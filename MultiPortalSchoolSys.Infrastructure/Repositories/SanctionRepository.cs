using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.HR;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class SanctionRepository : Repository<Sanction>, ISanctionRepository
{
    public SanctionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Sanction>> GetByUserAsync(string userId)
        => await _context.Sanctions
            .Where(s => s.IssuedToUserId == userId)
            .OrderByDescending(s => s.IssuedAt)
            .ToListAsync();

    public async Task<IEnumerable<Sanction>> GetActiveAsync()
        => await _context.Sanctions
            .Where(s => !s.IsResolved &&
                        (s.ExpiresAt == null || s.ExpiresAt > DateTime.UtcNow))
            .OrderByDescending(s => s.IssuedAt)
            .ToListAsync();
}