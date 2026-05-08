using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class StudentAttendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public ClassRoom? ClassRoom { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty; // Present, Absent, Late
    }
}