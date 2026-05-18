using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Student>> GetByClassAsync(int classRoomId)
        => await _context.Students
            .Include(s => s.ClassRoom)
            .Where(s => s.ClassRoomId == classRoomId)
            .OrderBy(s => s.AdmissionNo)
            .ToListAsync();

    public async Task<Student?> GetWithParentAsync(int studentId)
        => await _context.Students
            .Include(s => s.Parent)
            .FirstOrDefaultAsync(s => s.Id == studentId);

    public async Task<Student?> GetWithDetailsAsync(int studentId)
        => await _context.Students
            .Include(s => s.Parent)
            .Include(s => s.ClassRoom)
                .ThenInclude(c => c!.FormTeacher)
            .FirstOrDefaultAsync(s => s.Id == studentId);

    public async Task<IEnumerable<Student>> SearchByNameAsync(string name)
        => await _context.Students
            .Where(s => s.AdmissionNo.Contains(name))
            .ToListAsync();

    public async Task<Student?> GetByAdmissionNoAsync(string admissionNo)
        => await _context.Students
            .FirstOrDefaultAsync(s => s.AdmissionNo == admissionNo);

    public async Task<Student?> GetByUserIdAsync(string userId)
        => await _context.Students
            .Include(s => s.Parent)
            .Include(s => s.ClassRoom)
            .FirstOrDefaultAsync(s => s.UserId == userId);
}