using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class CbtExam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty; // e.g., "Mid-Term Mathematics"

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }

        public int DurationMinutes { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal TotalMarks { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Property: All questions in this exam
        public ICollection<CbtQuestion> Questions { get; set; } = new List<CbtQuestion>();
    }
}