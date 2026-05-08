using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        // Links to their Identity Login
        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(20)]
        public string StaffNo { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        public string? Qualifications { get; set; }

        // public int? DepartmentId { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}