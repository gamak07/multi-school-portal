namespace MultiPortalSchoolSys.Application.DTOs.Student;
public class StudentDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AdmissionNo { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string? ClassName { get; set; }
    public string? ParentName { get; set; }
    public string? ParentPhone { get; set; }
    public string Status { get; set; } = string.Empty;
}
