using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class PaymentReceipt
    {
        [Key]
        public int Id { get; set; }

        public int FeeInvoiceId { get; set; }
        [ForeignKey("FeeInvoiceId")]
        public FeeInvoice? FeeInvoice { get; set; }

        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Parent? Parent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // e.g., "Card", "Bank Transfer", "Cash"

        [MaxLength(100)]
        public string ReferenceNo { get; set; } = string.Empty; // Transaction ID from the payment gateway
    }
}