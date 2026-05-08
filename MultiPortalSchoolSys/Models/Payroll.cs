using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class Payroll
    {
        [Key]
        public int Id { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }

        [Required]
        [MaxLength(20)]
        public string Month { get; set; } = string.Empty; // e.g., "October 2025"

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPay { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}