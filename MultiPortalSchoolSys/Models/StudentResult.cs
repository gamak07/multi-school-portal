using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class StudentResult
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }

        [Required]
        [MaxLength(20)]
        public string AcademicYear { get; set; } = string.Empty; // e.g., "2025/2026"

        [Required]
        public int Term { get; set; } // 1 (First), 2 (Second), or 3 (Third)

        [Column(TypeName = "decimal(5,2)")]
        public decimal CAScore { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ExamScore { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal TotalScore { get; set; } // CA + Exam

        [MaxLength(2)]
        public string Grade { get; set; } = string.Empty; // A, B, C, F

        [MaxLength(100)]
        public string Remark { get; set; } = string.Empty; // e.g., "Excellent", "Needs Improvement"
        
        public bool IsPublished { get; set; } = false;
    }
}