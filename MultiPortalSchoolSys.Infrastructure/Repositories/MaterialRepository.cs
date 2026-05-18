using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Content;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class MaterialRepository : Repository<Material>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Material>> GetBySubjectAndClassAsync(int subjectId, int classRoomId)
        => await _context.Materials
            .Include(m => m.Subject)
            .Include(m => m.Teacher)
            .Where(m => m.SubjectId == subjectId &&
                        m.Subject!.ClassId == classRoomId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Material>> GetByTeacherAsync(int teacherId)
        => await _context.Materials
            .Include(m => m.Subject)
            .Where(m => m.UploadedBy == teacherId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
}