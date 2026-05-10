using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class MaterialRepository : Repository<Material>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Returns all materials for a subject that belongs to a specific classroom.
    /// Joins through Subject.ClassId since Material has no direct ClassRoomId.
    /// Used by the Student portal to list downloadable resources for their class.
    /// </summary>
    public async Task<IEnumerable<Material>> GetBySubjectAndClassAsync(int subjectId, int classRoomId)
        => await _context.Materials
            .Include(m => m.Subject)
            .Include(m => m.Teacher)
                .ThenInclude(t => t!.User)
            .Where(m => m.SubjectId == subjectId &&
                        m.Subject!.ClassId == classRoomId)
            .OrderByDescending(m => m.UploadedAt)
            .ToListAsync();

    /// <summary>
    /// Returns all materials uploaded by a specific teacher.
    /// Used on the Teacher portal to manage their own uploaded resources.
    /// </summary>
    public async Task<IEnumerable<Material>> GetByTeacherAsync(int teacherId)
        => await _context.Materials
            .Include(m => m.Subject)
            .Where(m => m.UploadedBy == teacherId)
            .OrderByDescending(m => m.UploadedAt)
            .ToListAsync();
}