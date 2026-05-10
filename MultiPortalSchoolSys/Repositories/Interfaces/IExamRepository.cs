using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IExamRepository : IRepository<CbtExam>
{
    Task<IEnumerable<CbtExam>> GetActiveExamsAsync();
    Task<CbtExam?> GetWithQuestionsAsync(int examId);
    Task<IEnumerable<CbtExam>> GetByClassAndSubjectAsync(int classRoomId, int subjectId);
}