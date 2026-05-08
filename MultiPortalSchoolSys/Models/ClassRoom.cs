using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class ClassRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty; // e.g., "JSS 1A" or "Grade 10"

        // Optional: The teacher assigned to manage this specific class
        public int? FormTeacherId { get; set; }
        [ForeignKey("FormTeacherId")]
        public Teacher? FormTeacher { get; set; }

        [MaxLength(10)]
        public string Arm { get; set; } = string.Empty; // e.g., "A", "B", "Science"

        // public int? SessionId { get; set; } 
        // Navigation Property: All students in this class
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}