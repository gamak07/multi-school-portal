using MultiPortalSchoolSys.Domain.Entities.Assessment;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ICbtQuestionRepository : IRepository<CbtQuestion>
{
    // Query 1: Fetch the complete question paper for a specific test
    Task<IEnumerable<CbtQuestion>> GetByExamIdAsync(int examId);

    // Query 2: Fetch a single question along with its parent exam configuration details
    Task<CbtQuestion?> GetWithExamByIdAsync(int questionId);
}