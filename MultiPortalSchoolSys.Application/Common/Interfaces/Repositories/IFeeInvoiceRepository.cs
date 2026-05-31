using MultiPortalSchoolSys.Domain.Entities.Finance;

namespace MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;

public interface IFeeInvoiceRepository : IRepository<FeeInvoice>
{
    // Query 1: Fetch an individual invoice along with its complete historical payments log
    Task<FeeInvoice?> GetWithPaymentsByIdAsync(int id);

    // Query 2: Retrieve all invoices assigned to a single student for a specific academic term
    Task<IEnumerable<FeeInvoice>> GetByStudentAndTermAsync(int studentId, int academicTermId);

    // Query 3: Admin Ledger lookup to track collections and debtors across a target term slot
    Task<IEnumerable<FeeInvoice>> GetWithStudentByTermAndStatusAsync(int academicTermId, bool isPaid);
}