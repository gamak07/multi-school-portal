using MultiPortalSchoolSys.Domain.Entities.Assessment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ITheoryQuestionRepository : IRepository<TheoryQuestion>
{
    // Query 1: Pull the complete question list for a specific theory exam paper
    Task<IEnumerable<TheoryQuestion>> GetByTheoryExamIdAsync(int theoryExamId);

    // Query 2: Fetch an individual question and eagerly load its parent exam rules
    Task<TheoryQuestion?> GetWithTheoryExamByIdAsync(int questionId);
}