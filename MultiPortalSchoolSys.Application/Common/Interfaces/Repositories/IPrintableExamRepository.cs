using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IPrintableExamRepository : IRepository<PrintableExam>
{
    // Query 1: Load a specific printable exam record with its fully populated relationship data
    Task<PrintableExam?> GetWithDetailsByIdAsync(int id);

    // Query 2: Fetch all exam uploads created by a specific teacher
    Task<IEnumerable<PrintableExam>> GetByTeacherIdAsync(int teacherId);

    // Query 3: Find approved downloadable exams for a specific classroom channel
    Task<IEnumerable<PrintableExam>> GetBySubjectAndTermAsync(int subjectId, int academicTermId);

    // Query 4: Pull rows matching a specific step in the validation process
    Task<IEnumerable<PrintableExam>> GetByApprovalStatusAsync(ExamApprovalStatus status);
}