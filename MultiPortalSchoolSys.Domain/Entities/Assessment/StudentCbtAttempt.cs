using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class StudentCbtAttempt : BaseEntity
{
    private StudentCbtAttempt() { }
    public int StudentId { get; private set; }
    // [ForeignKey("StudentId")]
    public Student? Student { get; private set; }

    public int ExamId { get; private set; }
    // [ForeignKey("ExamId")]
    public CbtExam? Exam { get; private set; }

    public DateTime StartTime { get; private set; } = DateTime.UtcNow;

    // Updated every 30 seconds by JS auto-save timer
    public DateTime? AutoSavedSubmitTime { get; private set; }

    // [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; private set; } = 0;

    // Once true, student cannot re-enter the exam
    public bool IsCompleted { get; private set; } = false;

    public StudentCbtAttempt(int studentId, int examId)
    {

        if (studentId <= 0) throw new ArgumentException("Invalid student ID.", nameof(studentId));
        if (examId <= 0) throw new ArgumentException("Invalid exam ID.", nameof(examId));



        StudentId = studentId;
        ExamId = examId;
        IsCompleted = false;
        Score = 0;
    }

    public void AutoSaveProgress()
    {
        if (IsCompleted)
        {
            throw new InvalidOperationException("Cannot auto-save progress for a completed attempt.");
        }
        AutoSavedSubmitTime = DateTime.UtcNow;
    }

    public void CompleteAttempt(decimal finalScore)
    {
        if (IsCompleted)
        {
            throw new InvalidOperationException("Attempt is already completed.");
        }
        if (finalScore < 0) throw new ArgumentException("Final score cannot be negative.", nameof(finalScore));

        Score = finalScore;
        IsCompleted = true;
        AutoSavedSubmitTime = DateTime.UtcNow;
    }
}
