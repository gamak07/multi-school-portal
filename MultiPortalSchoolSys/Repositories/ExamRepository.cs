using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class ExamRepository : Repository<CbtExam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Returns all active exams whose window has started but not yet ended.
    /// Used by the Student portal to show the "Available Exams" list.
    /// Business rule: IsActive must be true AND current time must be within StartTime–EndTime.
    /// </summary>
    public async Task<IEnumerable<CbtExam>> GetActiveExamsAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.CbtExams
            .Include(e => e.Subject)
            .Where(e => e.IsActive &&
                        e.StartTime <= now &&
                        e.EndTime >= now)
            .OrderBy(e => e.EndTime) // Soonest-to-expire first
            .ToListAsync();
    }

    /// <summary>
    /// Returns a single exam with ALL its questions loaded.
    /// This is the query called when a student opens the exam page.
    /// Questions are shuffled in the Service layer, not here.
    /// </summary>
    public async Task<CbtExam?> GetWithQuestionsAsync(int examId)
        => await _context.CbtExams
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .FirstOrDefaultAsync(e => e.Id == examId);

    /// <summary>
    /// Returns all exams for a specific class and subject.
    /// Joins through Subject.ClassId since CbtExam has no direct ClassRoomId.
    /// Used by teachers to manage exams for their assigned subjects.
    /// </summary>
    public async Task<IEnumerable<CbtExam>> GetByClassAndSubjectAsync(int classRoomId, int subjectId)
        => await _context.CbtExams
            .Include(e => e.Subject)
            .Where(e => e.SubjectId == subjectId &&
                        e.Subject!.ClassId == classRoomId)
            .OrderByDescending(e => e.StartTime)
            .ToListAsync();
}