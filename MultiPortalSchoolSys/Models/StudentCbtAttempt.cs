using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class StudentCbtAttempt
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int ExamId { get; set; }
        [ForeignKey("ExamId")]
        public CbtExam? Exam { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        
        // This is where your JS timer will ping its updates
        public DateTime? AutoSavedSubmitTime { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Score { get; set; } = 0;

        // If true, the student cannot re-enter the exam
        public bool IsCompleted { get; set; } = false; 
    }
}