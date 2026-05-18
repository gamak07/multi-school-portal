using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Domain.Entities.Finance;
using MultiPortalSchoolSys.Infrastructure.Data;

namespace MultiPortalSchoolSys.Infrastructure.Repositories;

public class FeeRepository : Repository<FeeInvoice>, IFeeRepository
{
    public FeeRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<FeeInvoice>> GetOutstandingByStudentAsync(int studentId)
        => await _context.FeeInvoices
            .Where(f => f.StudentId == studentId && !f.IsPaid)
            .OrderBy(f => f.DueDate)
            .ToListAsync();

    public async Task<IEnumerable<PaymentReceipt>> GetPaymentHistoryAsync(int studentId)
        => await _context.PaymentReceipts
            .Include(r => r.FeeInvoice)
            .Where(r => r.FeeInvoice!.StudentId == studentId)
            .OrderByDescending(r => r.PaymentDate)
            .ToListAsync();

    public async Task<IEnumerable<FeeInvoice>> GetBySessionTermAsync(string sessionTerm)
        => await _context.FeeInvoices
            .Include(f => f.Student)
            .Where(f => f.SessionTerm == sessionTerm)
            .OrderBy(f => f.Student!.AdmissionNo)
            .ToListAsync();
}