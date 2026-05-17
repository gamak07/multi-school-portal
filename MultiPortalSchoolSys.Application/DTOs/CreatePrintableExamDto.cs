using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class CreatePrintableExamDto
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public int SubjectId { get; set; }
    [Required] public int CreatedByTeacherId { get; set; }
    [Required] public string DocumentUrl { get; set; } = string.Empty;
    [Required] public string AcademicYear { get; set; } = string.Empty;
    [Required] public int Term { get; set; }
}
