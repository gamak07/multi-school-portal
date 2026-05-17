using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Student;
public class CreateStudentDto
{
    [Required][MaxLength(20)]
    public string AdmissionNo { get; set; } = string.Empty;
    [Required]
    public int ParentId { get; set; }
    public int? ClassRoomId { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    // UserId is set by AuthService during account provisioning
    [Required]
    public string UserId { get; set; } = string.Empty;
}
