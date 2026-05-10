using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IResultRepository : IRepository<StudentResult>
{
    /// <summary>
    /// Returns all results for a student in a specific term.
    /// PHASE D FIX: Parameter renamed from 'termId' to 'term' (int value: 1, 2, or 3)
    /// because StudentResult.Term is a plain integer, not a foreign key to a Term table.
    /// </summary>
    Task<IEnumerable<StudentResult>> GetByStudentAndTermAsync(int studentId, int term);

    /// <summary>
    /// Returns all results for every student in a classroom for a given term.
    /// Used by teachers and admin to view/edit the full class result sheet.
    /// </summary>
    Task<IEnumerable<StudentResult>> GetClassResultsAsync(int classRoomId, int term);

    /// <summary>
    /// Returns only published results for a classroom and term.
    /// PHASE D FIX: Now implementable because StudentResult.IsPublished has been added.
    /// Used by the Student and Parent portals — they must NEVER see unpublished results.
    /// </summary>
    Task<IEnumerable<StudentResult>> GetPublishedAsync(int classRoomId, int term);
}