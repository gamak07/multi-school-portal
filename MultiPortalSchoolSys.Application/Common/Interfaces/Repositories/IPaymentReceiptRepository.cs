using MultiPortalSchoolSys.Domain.Entities.Finance;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IPaymentReceiptRepository : IRepository<PaymentReceipt>
{
    // Query 1: Payment gateway hook verification lookup using alternate business tracking key
    Task<PaymentReceipt?> GetByReferenceNoAsync(string referenceNo);

    // Query 2: Fetch a single receipt complete with nested Invoice, Student, and Parent context
    Task<PaymentReceipt?> GetWithDetailsByIdAsync(int id);

    // Query 3: Retrieve all payment records filed under a single transaction invoice anchor
    Task<IEnumerable<PaymentReceipt>> GetByFeeInvoiceIdAsync(int feeInvoiceId);

    // Query 4: Fetch a history wallet collection of all payments completed by a specific parent
    Task<IEnumerable<PaymentReceipt>> GetWithInvoiceByParentIdAsync(int parentId);
}