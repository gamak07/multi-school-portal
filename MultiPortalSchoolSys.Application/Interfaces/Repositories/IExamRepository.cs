using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IExamRepository : IRepository<CbtExam>
{
    Task<IEnumerable<CbtExam>> GetActiveExamsAsync();
    Task<CbtExam?> GetWithQuestionsAsync(int examId);
    Task<IEnumerable<CbtExam>> GetByClassAndSubjectAsync(int classRoomId, int subjectId);
    Task<IEnumerable<CbtExam>> GetByApprovalStatusAsync(ExamApprovalStatus status);
    Task<IEnumerable<TheoryExam>> GetTheoryExamsByStatusAsync(ExamApprovalStatus status);
    Task<IEnumerable<PrintableExam>> GetPrintableExamsByStatusAsync(ExamApprovalStatus status);
}