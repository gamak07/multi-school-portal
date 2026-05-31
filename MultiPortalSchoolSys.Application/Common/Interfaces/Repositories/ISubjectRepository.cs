using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ISubjectRepository : IRepository<Subject>
{
    // 1. Alternate Key Lookup: Used when verifying codes or clicking unique subject links
    Task<Subject?> GetByCodeAsync(string code);

    // 2. Eager-Loading Lookup: Grabs the subject and joins its teacher and classroom profiles
    Task<Subject?> GetWithClassAndTeacherAsync(int subjectId);

    // 3. Teacher Allocation Query: Returns a LIST of all subjects a specific teacher handles
    Task<IEnumerable<Subject>> GetByTeacherIdAsync(int teacherId);

    // 4. Classroom Curriculum Query: Returns a LIST of all subjects assigned to a specific class layout
    Task<IEnumerable<Subject>> GetByClassIdAsync(int classId);

    // 5. Global Filter: Returns all active subjects across the school system
    Task<IEnumerable<Subject>> GetAllActiveAsync();
}