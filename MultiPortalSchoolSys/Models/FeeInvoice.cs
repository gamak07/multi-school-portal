using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class FeeInvoice
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty; // e.g., "Term 1 Tuition Fees"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsPaid { get; set; } = false;

        // Navigation Property: A single invoice could have multiple partial payments
        public ICollection<PaymentReceipt> Payments { get; set; } = new List<PaymentReceipt>();
    }
}