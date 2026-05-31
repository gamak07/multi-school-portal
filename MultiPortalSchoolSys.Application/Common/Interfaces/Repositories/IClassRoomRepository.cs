using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IClassRoomRepository : IRepository<ClassRoom>
{
    // Eager-loading queries (Fetch by Classroom ID, include related details)
    Task<ClassRoom?> GetWithStudentsAsync(int classRoomId);
    Task<ClassRoom?> GetWithFormTeacherAsync(int classRoomId);
    
    // Often, you might want a single method that eager-loads BOTH collections for a full dashboard view
    Task<ClassRoom?> GetWithDetailsAsync(int classRoomId);

    // Filter lookups (Fetch the single classroom that owns these specific child records)
    Task<ClassRoom?> GetByFormTeacherIdAsync(int formTeacherId);
    Task<ClassRoom?> GetByStudentIdAsync(int studentId);
    Task<ClassRoom?> GetBySubjectIdAsync(int subjectId);

}