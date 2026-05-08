using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class StudentAnswer
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int ExamId { get; set; }
        [ForeignKey("ExamId")]
        public CbtExam? Exam { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public CbtQuestion? Question { get; set; }

        [MaxLength(1)]
        public string? SelectedOption { get; set; } // A, B, C, or D

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}