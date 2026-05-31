using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ITheoryExamRepository : IRepository<TheoryExam>
{
    // Query 1: Fetch a specific theory exam paper complete with all its questions loaded
    Task<TheoryExam?> GetWithQuestionsByIdAsync(int id);

    // Query 2: Fetch all theory exam papers created by a specific instructor
    Task<IEnumerable<TheoryExam>> GetByTeacherIdAsync(int teacherId);

    // Query 3: Find active theory exams allocated to a specific subject channel and term session
    Task<IEnumerable<TheoryExam>> GetBySubjectAndTermAsync(int subjectId, int academicTermId);

    // Query 4: Pull rows matching a specific validation pipeline checkpoint state
    Task<IEnumerable<TheoryExam>> GetByApprovalStatusAsync(ExamApprovalStatus status);
}