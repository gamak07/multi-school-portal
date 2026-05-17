using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Teacher;
public class CreateTeacherDto
{
    [Required][MaxLength(20)]
    public string StaffNo { get; set; } = string.Empty;
    [Required]
    public DateTime HireDate { get; set; }
    public decimal BasicSalary { get; set; }
    public string? Qualifications { get; set; }
    [Required]
    public string UserId { get; set; } = string.Empty;
}
