using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class StudentCbtAttempt : BaseEntity
{
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    public int ExamId { get; set; }
    [ForeignKey("ExamId")]
    public CbtExam? Exam { get; set; }

    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    // Updated every 30 seconds by JS auto-save timer
    public DateTime? AutoSavedSubmitTime { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; set; } = 0;

    // Once true, student cannot re-enter the exam
    public bool IsCompleted { get; set; } = false;
}
