using MultiPortalSchoolSys.Domain.Entities.Finance;

namespace MultiPortalSchoolSys.Application.Interfaces.Repositories;

public interface IFeeRepository : IRepository<FeeInvoice>
{
    Task<IEnumerable<FeeInvoice>> GetOutstandingByStudentAsync(int studentId);
    Task<IEnumerable<PaymentReceipt>> GetPaymentHistoryAsync(int studentId);
    Task<IEnumerable<FeeInvoice>> GetBySessionTermAsync(string sessionTerm);
}