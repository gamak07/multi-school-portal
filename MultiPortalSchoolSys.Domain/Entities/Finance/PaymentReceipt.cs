using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Finance;

public class PaymentReceipt : BaseEntity
{
    public int FeeInvoiceId { get; private set; }
    // [ForeignKey("FeeInvoiceId")]
    public FeeInvoice? FeeInvoice { get; private set; }

    public int ParentId { get; private set; }
    // [ForeignKey("ParentId")]
    public Parent? Parent { get; private set; }

    // [Column(TypeName = "decimal(18,2)")]
    public decimal AmountPaid { get; private set; }

    public DateTime PaymentDate { get; private set; } = DateTime.UtcNow;

    // [Required]
    // [MaxLength(50)]
    public string PaymentMethod { get; private set; } = string.Empty; // "Card", "Bank Transfer", "Cash"

    // [MaxLength(100)]
    public string ReferenceNo { get; private set; } = string.Empty;
    public bool IsVoided { get; private set; } = false;
    public string? VoidReason { get; private set; }

    private PaymentReceipt() { }

    public PaymentReceipt(int feeInvoiceId, int parentId, decimal amountPaid, string paymentMethod, string referenceNo)
    {
        if (feeInvoiceId <= 0) throw new ArgumentException("Invalid fee invoice ID.", nameof(feeInvoiceId));
        if (parentId <= 0) throw new ArgumentException("Invalid parent ID.", nameof(parentId));
        if (amountPaid <= 0) throw new ArgumentException("Amount paid must be greater than zero.", nameof(amountPaid));
        if (string.IsNullOrWhiteSpace(referenceNo)) throw new ArgumentException("Reference number cannot be empty.", nameof(referenceNo));
        if (string.IsNullOrWhiteSpace(paymentMethod)) throw new ArgumentException("Payment method cannot be empty.", nameof(paymentMethod));

        FeeInvoiceId = feeInvoiceId;
        ParentId = parentId;
        AmountPaid = amountPaid;
        PaymentMethod = paymentMethod.Trim();
        ReferenceNo = referenceNo.Trim();
    }

    public void VoidPayment(string reason)
    {
        if (IsVoided) throw new InvalidOperationException("Payment is already voided.");
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Void reason cannot be empty.", nameof(reason));

        IsVoided = true;
        VoidReason = reason.Trim();
    }
}
