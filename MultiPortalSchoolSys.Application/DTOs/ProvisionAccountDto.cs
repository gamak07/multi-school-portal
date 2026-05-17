using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Auth;
public class ProvisionAccountDto
{
    [Required][MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required][MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [Required][EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Role { get; set; } = string.Empty;
    public string? StaffNo { get; set; }
    public string? AdmissionNo { get; set; }
    public int? ParentId { get; set; }
    public int? ClassRoomId { get; set; }
    public string? Occupation { get; set; }
    public string? HomeAddress { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? HireDate { get; set; }
    public decimal? BasicSalary { get; set; }
    public string? Qualifications { get; set; }
}
