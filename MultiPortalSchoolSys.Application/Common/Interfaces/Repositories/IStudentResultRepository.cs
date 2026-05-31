
using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IStudentResultRepository : IRepository<StudentResult>
{
    Task<StudentResult?> GetByStudentSubjectTermAsync(int studentId, int subjectId, int academicTermId);
    Task<IEnumerable<StudentResult>> GetWithSubjectByStudentAndTermAsync(int studentId, int academicTermId);
    Task<IEnumerable<StudentResult>> GetWithStudentBySubjectAndTermAsync(int subjectId, int academicTermId);
}