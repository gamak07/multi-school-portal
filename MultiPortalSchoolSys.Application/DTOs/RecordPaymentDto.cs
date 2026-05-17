using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Fee;
public class RecordPaymentDto
{
    [Required] public int FeeInvoiceId { get; set; }
    [Required] public int ParentId { get; set; }
    [Required] public decimal AmountPaid { get; set; }
    [Required] public string PaymentMethod { get; set; } = string.Empty;
    public string? ReferenceNo { get; set; }
}
