using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// PHASE B FIX: Added .Include(s => s.User) so we can order by User.LastName.
    /// Student has no FirstName/LastName of its own — those live on ApplicationUser.
    /// </summary>
    public async Task<IEnumerable<Student>> GetByClassAsync(int classRoomId)
        => await _context.Students
            .Include(s => s.User)
            .Where(s => s.ClassRoomId == classRoomId)
            .OrderBy(s => s.User!.LastName)
            .ToListAsync();

    public async Task<Student?> GetWithParentAsync(int studentId)
        => await _context.Students
            .Include(s => s.Parent)
                .ThenInclude(p => p!.User)
            .FirstOrDefaultAsync(s => s.Id == studentId);

    /// <summary>
    /// PHASE B ADDITION: Full aggregate query.
    /// Loads Student + all related navigation objects in one round-trip to the database.
    /// The Service layer will use this for the student profile/dashboard page.
    /// </summary>
    public async Task<Student?> GetWithDetailsAsync(int studentId)
        => await _context.Students
            .Include(s => s.User)
            .Include(s => s.Parent)
                .ThenInclude(p => p!.User)
            .Include(s => s.ClassRoom)
                .ThenInclude(c => c!.FormTeacher)
                    .ThenInclude(t => t!.User)
            .FirstOrDefaultAsync(s => s.Id == studentId);

    /// <summary>
    /// PHASE B FIX: Corrected predicate to navigate through s.User.
    /// Original code referenced s.FirstName and s.LastName which do not exist on Student.
    /// Also guards against a null User with the null-conditional check.
    /// </summary>
    public async Task<IEnumerable<Student>> SearchByNameAsync(string name)
        => await _context.Students
            .Include(s => s.User)
            .Where(s => s.User != null &&
                        (s.User.FirstName.Contains(name) ||
                         s.User.LastName.Contains(name)))
            .ToListAsync();

    public async Task<Student?> GetByAdmissionNoAsync(string admissionNo)
        => await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.AdmissionNo == admissionNo);
}