using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Fee;
public class CreateFeeInvoiceDto
{
    [Required] public int StudentId { get; set; }
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public decimal Amount { get; set; }
    [Required] public DateTime DueDate { get; set; }
    [Required] public string SessionTerm { get; set; } = string.Empty;
}
