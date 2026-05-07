using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        // Links to their Identity Login (if students log in directly)
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        // Links to the Parent
        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Parent? Parent { get; set; }

        public int? ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")]
        public ClassRoom? ClassRoom { get; set; }

        [Required]
        [MaxLength(20)]
        public string AdmissionNo { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}