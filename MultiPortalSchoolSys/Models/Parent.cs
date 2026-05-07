using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        // Links to their Identity Login
        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [MaxLength(100)]
        public string? Occupation { get; set; }

        [MaxLength(200)]
        public string? HomeAddress { get; set; }

        // Navigation Property: A parent can have multiple students
        public ICollection<Student> Children { get; set; } = new List<Student>();
    }
}