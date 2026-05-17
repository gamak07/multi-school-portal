using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class CreateTheoryExamDto
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public int SubjectId { get; set; }
    [Required] public int CreatedByTeacherId { get; set; }
    [Required] public string AcademicYear { get; set; } = string.Empty;
    [Required] public int Term { get; set; }
    public decimal TotalMarks { get; set; }
}
