using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class CreateCbtExamDto
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public int SubjectId { get; set; }
    [Required] public int CreatedByTeacherId { get; set; }
    [Required] public int DurationMinutes { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
    public decimal TotalMarks { get; set; }
}
