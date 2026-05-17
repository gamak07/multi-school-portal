namespace MultiPortalSchoolSys.Application.DTOs.Payroll;
public class PayrollDto
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string StaffNo { get; set; } = string.Empty;
    public string Month { get; set; } = string.Empty;
    public decimal GrossSalary { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetPay { get; set; }
    public DateTime? PaidAt { get; set; }
}
