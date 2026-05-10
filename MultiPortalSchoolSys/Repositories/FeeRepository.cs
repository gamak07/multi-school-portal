using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

public class FeeRepository : Repository<FeeInvoice>, IFeeRepository
{
    public FeeRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Returns all unpaid invoices for a student, ordered by due date.
    /// Used on the Parent portal to show what is owed.
    /// </summary>
    public async Task<IEnumerable<FeeInvoice>> GetOutstandingByStudentAsync(int studentId)
        => await _context.FeeInvoices
            .Where(f => f.StudentId == studentId && f.IsPaid == false)
            .OrderBy(f => f.DueDate)
            .ToListAsync();

    /// <summary>
    /// Returns full payment receipt history for a student.
    /// Joins PaymentReceipt → FeeInvoice → Student since PaymentReceipt
    /// only has a ParentId and FeeInvoiceId, not a direct StudentId.
    /// </summary>
    public async Task<IEnumerable<PaymentReceipt>> GetPaymentHistoryAsync(int studentId)
        => await _context.PaymentReceipts
            .Include(r => r.FeeInvoice)
            .Where(r => r.FeeInvoice!.StudentId == studentId)
            .OrderByDescending(r => r.PaymentDate)
            .ToListAsync();

    /// <summary>
    /// PHASE C FIX: Renamed from GetBySessionAsync(int sessionId) to
    /// GetBySessionTermAsync(string sessionTerm) to match FeeInvoice.SessionTerm
    /// which is a string property (e.g., "2025/2026 Term 1").
    /// Used by Admin to generate a full fee report for a given term.
    /// </summary>
    public async Task<IEnumerable<FeeInvoice>> GetBySessionTermAsync(string sessionTerm)
        => await _context.FeeInvoices
            .Include(f => f.Student)
                .ThenInclude(s => s!.User)
            .Where(f => f.SessionTerm == sessionTerm)
            .OrderBy(f => f.Student!.User!.LastName)
            .ToListAsync();
}