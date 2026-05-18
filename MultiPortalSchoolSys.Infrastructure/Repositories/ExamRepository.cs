using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Enums;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class ExamRepository : Repository<CbtExam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<CbtExam>> GetActiveExamsAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.CbtExams
            .Include(e => e.Subject)
            .Where(e => e.IsActive &&
                        e.ApprovalStatus == ExamApprovalStatus.Approved &&
                        e.StartTime <= now &&
                        e.EndTime >= now)
            .OrderBy(e => e.EndTime)
            .ToListAsync();
    }

    public async Task<CbtExam?> GetWithQuestionsAsync(int examId)
        => await _context.CbtExams
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .FirstOrDefaultAsync(e => e.Id == examId);

    public async Task<IEnumerable<CbtExam>> GetByClassAndSubjectAsync(int classRoomId, int subjectId)
        => await _context.CbtExams
            .Include(e => e.Subject)
            .Where(e => e.SubjectId == subjectId &&
                        e.Subject!.ClassId == classRoomId)
            .OrderByDescending(e => e.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<CbtExam>> GetByApprovalStatusAsync(ExamApprovalStatus status)
        => await _context.CbtExams
            .Include(e => e.Subject)
            .Include(e => e.CreatedByTeacher)
            .Where(e => e.ApprovalStatus == status)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<TheoryExam>> GetTheoryExamsByStatusAsync(ExamApprovalStatus status)
        => await _context.TheoryExams
            .Include(e => e.Subject)
            .Include(e => e.CreatedByTeacher)
            .Where(e => e.ApprovalStatus == status)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<PrintableExam>> GetPrintableExamsByStatusAsync(ExamApprovalStatus status)
        => await _context.PrintableExams
            .Include(e => e.Subject)
            .Include(e => e.CreatedByTeacher)
            .Where(e => e.ApprovalStatus == status)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
}