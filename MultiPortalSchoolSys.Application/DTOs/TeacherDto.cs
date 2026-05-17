namespace MultiPortalSchoolSys.Application.DTOs.Teacher;
public class TeacherDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string StaffNo { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal BasicSalary { get; set; }
    public string? Qualifications { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Subjects { get; set; } = new();
}
