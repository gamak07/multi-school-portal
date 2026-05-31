using MultiPortalSchoolSys.Domain.Entities.Assessment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStudentAnswerRepository : IRepository<StudentAnswer>
{
    // Query 1: Find a specific single answer slot to create or update an auto-save
    Task<StudentAnswer?> GetByStudentExamAndQuestionAsync(int studentId, int examId, int questionId);

    // Query 2: Fetch all answers a student provided for an exam, including the question keys for grading
    Task<IEnumerable<StudentAnswer>> GetWithQuestionByStudentAndExamAsync(int studentId, int examId);
}