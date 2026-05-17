using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Fee;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IFeeService
{
    Task<Result<IEnumerable<FeeInvoiceDto>>> GetOutstandingByStudentAsync(int studentId);
    Task<Result<IEnumerable<PaymentReceiptDto>>> GetPaymentHistoryAsync(int studentId);
    Task<Result<IEnumerable<FeeInvoiceDto>>> GetBySessionTermAsync(string sessionTerm);
    Task<Result<FeeInvoiceDto>> CreateInvoiceAsync(CreateFeeInvoiceDto dto);
    Task<Result<PaymentReceiptDto>> RecordPaymentAsync(RecordPaymentDto dto);
    Task<Result> DeleteInvoiceAsync(int invoiceId);
}