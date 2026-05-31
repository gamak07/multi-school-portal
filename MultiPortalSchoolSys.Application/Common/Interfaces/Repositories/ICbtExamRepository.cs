using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ICbtExamRepository : IRepository<CbtExam>
{
    // 1. Heavy Eager-Loading Lookup: Loads the exam, its questions, and parent subject info for tests
    Task<CbtExam?> GetWithDetailsAsync(int examId);

    // 2. Teacher Dashboard Query: Fetches all exams a specific teacher created (and joins the Subject name)
    Task<IEnumerable<CbtExam>> GetByTeacherIdAsync(int teacherId);

    // 3. Subject Curriculum Query: Fetches all exams attached to a specific subject channel
    Task<IEnumerable<CbtExam>> GetBySubjectIdAsync(int subjectId);

    // 4. Admin Queue Query: Fetches exams matching a specific status (e.g., 'Submitted' for review)
    Task<IEnumerable<CbtExam>> GetByApprovalStatusAsync(ExamApprovalStatus status);

    // 5. Active Dynamic Query: Fetches currently open exams where current time is between Start and End windows
    Task<IEnumerable<CbtExam>> GetActiveExamsAsync();
}