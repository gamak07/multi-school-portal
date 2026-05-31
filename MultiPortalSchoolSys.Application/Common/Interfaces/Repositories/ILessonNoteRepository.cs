using MultiPortalSchoolSys.Domain.Entities.Content;
using MultiPortalSchoolSys.Domain.Enums;


namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface ILessonNoteRepository : IRepository<LessonNote>
{
    // Query 1: Fetch a single lesson note along with all related parent metadata details
    Task<LessonNote?> GetWithDetailsByIdAsync(int id);

    // Query 2: Fetch a comprehensive list of all plans submitted by a specific teacher for a term
    Task<IEnumerable<LessonNote>> GetByTeacherAndTermAsync(int teacherId, int academicTermId);

    // Query 3: Fetch the administrative review pipeline matching a specific verification status
    Task<IEnumerable<LessonNote>> GetWithDetailsByApprovalStatusAsync(LessonNoteStatus status);

    // Query 4: Retrieve reading documents assigned to a specific subject channel for a term
    Task<IEnumerable<LessonNote>> GetBySubjectAndTermAsync(int subjectId, int academicTermId);
}