using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Exam;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IExamService
{
    // ── CBT ──────────────────────────────────────────────────────────────────
    Task<Result<ExamDto>> GetCbtExamByIdAsync(int examId);
    Task<Result<IEnumerable<ExamDto>>> GetActiveExamsAsync();
    Task<Result<IEnumerable<ExamDto>>> GetCbtExamsByStatusAsync(ExamApprovalStatus status);
    Task<Result<ExamDto>> CreateCbtExamAsync(CreateCbtExamDto dto);
    Task<Result> AddCbtQuestionAsync(int examId, CbtQuestionDto dto);
    Task<Result> SubmitCbtExamForApprovalAsync(int examId, int teacherId);

    // ── Theory ───────────────────────────────────────────────────────────────
    Task<Result<TheoryExamDto>> GetTheoryExamByIdAsync(int examId);
    Task<Result<TheoryExamDto>> CreateTheoryExamAsync(CreateTheoryExamDto dto);
    Task<Result> AddTheoryQuestionAsync(int examId, TheoryQuestionDto dto);
    Task<Result> SubmitTheoryExamForApprovalAsync(int examId, int teacherId);

    // ── Printable ─────────────────────────────────────────────────────────────
    Task<Result<PrintableExamDto>> GetPrintableExamByIdAsync(int examId);
    Task<Result<PrintableExamDto>> CreatePrintableExamAsync(CreatePrintableExamDto dto);
    Task<Result> SubmitPrintableExamForApprovalAsync(int examId, int teacherId);

    // ── Approval workflow (Admin only) ────────────────────────────────────────
    Task<Result> ApproveExamAsync(int examId, ExamType type, string adminId);
    Task<Result> RejectExamAsync(int examId, ExamType type, string adminId, string remarks);

    // ── CBT Attempt (Student) ─────────────────────────────────────────────────
    Task<Result<CbtAttemptDto>> StartAttemptAsync(int examId, int studentId);
    Task<Result> SaveAnswerAsync(SaveAnswerDto dto);
    Task<Result<decimal>> SubmitAttemptAsync(int attemptId, int studentId);
}