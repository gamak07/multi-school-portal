using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Finance;

public class FeeInvoice : BaseEntity
{
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; } = false;

    [Required]
    [MaxLength(50)]
    public string SessionTerm { get; set; } = string.Empty; // e.g., "2025/2026 Term 1"

    public ICollection<PaymentReceipt> Payments { get; set; } = new List<PaymentReceipt>();
}
