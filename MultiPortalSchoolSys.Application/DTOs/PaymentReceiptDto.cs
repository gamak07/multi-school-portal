namespace MultiPortalSchoolSys.Application.DTOs.Fee;
public class PaymentReceiptDto
{
    public int Id { get; set; }
    public int FeeInvoiceId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string ReferenceNo { get; set; } = string.Empty;
}
