using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Finance;

public class Payroll : BaseEntity
{
    public int TeacherId { get; private set; }
    // [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; private set; }

    // [Required]
    // [MaxLength(20)]
    public string Month { get; private set; } = string.Empty; // e.g., "May 2026"

    // [Column(TypeName = "decimal(18,2)")]
    public decimal GrossSalary { get; private set; }

    // [Column(TypeName = "decimal(18,2)")]
    public decimal Deductions { get; private set; }

    // [Column(TypeName = "decimal(18,2)")]
    public decimal NetPay { get; private set; }

    public DateTime? PaidAt { get; private set; }
    public bool IsDisbursed => PaidAt.HasValue;

    private Payroll() { }

    public Payroll(int teacherId, string month, decimal grossSalary, decimal deductions)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));

        TeacherId = teacherId;

        UpdateSalaryDetails(month, grossSalary, deductions);
    }

    public void UpdateSalaryDetails(string month, decimal grossSalary, decimal deductions)
    {
        if (IsDisbursed)
            throw new InvalidOperationException("Cannot modify salary calculations after payroll has been disbursed.");

        if (string.IsNullOrWhiteSpace(month)) throw new ArgumentException("Payroll month cannot be empty.", nameof(month));
        if (grossSalary <= 0) throw new ArgumentException("Gross salary must be greater than zero.", nameof(grossSalary));
        if (deductions < 0) throw new ArgumentException("Deductions cannot be negative.", nameof(deductions));
        if (deductions > grossSalary) throw new ArgumentException("Deductions cannot exceed the total gross salary.");

        Month = month.Trim();
        GrossSalary = grossSalary;
        Deductions = deductions;

        // Internally calculate NetPay to enforce mathematical invariance
        NetPay = grossSalary - deductions;
    }
    public void MarkAsPaid()
    {
        if (IsDisbursed)
            throw new InvalidOperationException("This payroll record has already been settled and marked as paid.");

        PaidAt = DateTime.UtcNow;
    }
}
