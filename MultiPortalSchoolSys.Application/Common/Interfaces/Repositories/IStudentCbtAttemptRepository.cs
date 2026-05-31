using MultiPortalSchoolSys.Domain.Entities.Assessment;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStudentCbtAttemptRepository : IRepository<StudentCbtAttempt>
{
    // Query 1: Find a unique single session to resume or verify entry guards
    Task<StudentCbtAttempt?> GetByStudentAndExamAsync(int studentId, int examId);

    // Query 2: Fetch all exam submissions with student names for teacher review
    Task<IEnumerable<StudentCbtAttempt>> GetWithStudentByExamIdAsync(int examId);

    // Query 3: Fetch a student's entire history of test scores with exam titles
    Task<IEnumerable<StudentCbtAttempt>> GetWithExamByStudentIdAsync(int studentId);
}