using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Finance;

public class PaymentReceipt : BaseEntity
{
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
    public string PaymentMethod { get; set; } = string.Empty; // "Card", "Bank Transfer", "Cash"

    [MaxLength(100)]
    public string ReferenceNo { get; set; } = string.Empty;
}
