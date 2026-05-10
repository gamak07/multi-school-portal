using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IFeeRepository : IRepository<FeeInvoice>
{
    /// <summary>
    /// Returns all unpaid invoices for a specific student.
    /// </summary>
    Task<IEnumerable<FeeInvoice>> GetOutstandingByStudentAsync(int studentId);

    /// <summary>
    /// Returns the full payment receipt history for a student by joining
    /// PaymentReceipt → FeeInvoice → Student.
    /// </summary>
    Task<IEnumerable<PaymentReceipt>> GetPaymentHistoryAsync(int studentId);

    /// <summary>
    /// PHASE C FIX: Parameter changed from (int sessionId) to (string sessionTerm)
    /// to match the FeeInvoice.SessionTerm string property (e.g., "2025/2026 Term 1").
    /// An integer sessionId does not exist on the FeeInvoice model.
    /// </summary>
    Task<IEnumerable<FeeInvoice>> GetBySessionTermAsync(string sessionTerm);
}