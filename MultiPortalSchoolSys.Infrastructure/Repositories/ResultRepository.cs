using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class ResultRepository : Repository<StudentResult>, IResultRepository
{
    public ResultRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<StudentResult>> GetByStudentAndTermAsync(int studentId, int term)
        => await _context.StudentResults
            .Include(r => r.Subject)
            .Where(r => r.StudentId == studentId && r.Term == term)
            .OrderBy(r => r.Subject!.Name)
            .ToListAsync();

    public async Task<IEnumerable<StudentResult>> GetClassResultsAsync(int classRoomId, int term)
        => await _context.StudentResults
            .Include(r => r.Student)
            .Include(r => r.Subject)
            .Where(r => r.Subject!.ClassId == classRoomId && r.Term == term)
            .OrderBy(r => r.Student!.AdmissionNo)
            .ToListAsync();

    public async Task<IEnumerable<StudentResult>> GetPublishedAsync(int classRoomId, int term)
        => await _context.StudentResults
            .Include(r => r.Student)
            .Include(r => r.Subject)
            .Where(r => r.Subject!.ClassId == classRoomId &&
                        r.Term == term &&
                        r.IsPublished == true)
            .OrderBy(r => r.Student!.AdmissionNo)
            .ToListAsync();
}