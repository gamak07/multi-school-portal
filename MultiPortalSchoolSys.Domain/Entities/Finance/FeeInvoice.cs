using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Finance;

public class FeeInvoice : BaseEntity
{
    public int StudentId { get; private set; }
    public Student? Student { get; private set; }

    public int AcademicTermId { get; private set; }
    public AcademicTerm? AcademicTerm { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public decimal BalanceDue {get; private set;}
    public DateTime DueDate { get; private set; }
    public bool IsPaid { get; private set; } = false;
    public ICollection<PaymentReceipt> Payments { get; private set; } = [];

    private FeeInvoice() { }

    public FeeInvoice(int studentId, string description, decimal newAmount, DateTime dueDate, int academicTermId)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.", nameof(studentId));
        if (academicTermId <= 0) throw new ArgumentException("Invalid academic term ID.", nameof(academicTermId));
        StudentId = studentId;
        UpdateInvoice(description, newAmount, dueDate);
        AcademicTermId = academicTermId;
    }

    public void UpdateInvoice(string description, decimal newAmount, DateTime dueDate)
    {
        if(IsPaid)
            throw new InvalidOperationException("Cannot modify an invoice that has already been paid.");
        if(Payments.Count > 0)
            throw new InvalidOperationException("Cannot modify an invoice that has already received payments.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (newAmount <= 0) throw new ArgumentException("Amount cannot be negative.", nameof(newAmount));

        Description = description.Trim();
        Amount = newAmount;
        BalanceDue = newAmount; // Reset balance due to the new amount when updating
        DueDate = dueDate;
    }

    public void RecordPayment(decimal paymentAmount)
    {
        if (IsPaid || BalanceDue <= 0)
            throw new InvalidOperationException("This invoice is already marked as paid.");

        if (paymentAmount <= 0)
            throw new ArgumentException("Payment amount must be greater than zero.", nameof(paymentAmount));

        if (paymentAmount > BalanceDue)
            throw new ArgumentException($"Overpayment error. Remaining balance is {BalanceDue}, cannot apply a payment of {paymentAmount}.", nameof(paymentAmount));

        BalanceDue -= paymentAmount;

        if (BalanceDue == 0)
        {
            IsPaid = true;
        }
    }
}
