using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    public TeacherRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Teacher>> GetAllWithDetailsAsync()
        => await _context.Teachers
            .Include(t => t.Subjects)
            .OrderBy(t => t.StaffNo)
            .ToListAsync();

    public async Task<Teacher?> GetWithDetailsAsync(int teacherId)
        => await _context.Teachers
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.Id == teacherId);

    public async Task<Teacher?> GetByUserIdAsync(string userId)
        => await _context.Teachers
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.UserId == userId);

    public async Task<Teacher?> GetByStaffNoAsync(string staffNo)
        => await _context.Teachers
            .FirstOrDefaultAsync(t => t.StaffNo == staffNo);
}