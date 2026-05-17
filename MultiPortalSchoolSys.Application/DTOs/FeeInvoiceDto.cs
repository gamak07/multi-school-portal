namespace MultiPortalSchoolSys.Application.DTOs.Fee;
public class FeeInvoiceDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public string SessionTerm { get; set; } = string.Empty;
}
