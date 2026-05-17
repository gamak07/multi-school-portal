using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Calendar;
public class CreateAcademicTermDto
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string AcademicYear { get; set; } = string.Empty;
    [Required] public int TermNumber { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}
