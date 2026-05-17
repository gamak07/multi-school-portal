using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Result;
public class EnterResultDto
{
    [Required] public int StudentId { get; set; }
    [Required] public int SubjectId { get; set; }
    [Required] public string AcademicYear { get; set; } = string.Empty;
    [Required] public int Term { get; set; }
    [Range(0, 100)] public decimal CAScore { get; set; }
    [Range(0, 100)] public decimal ExamScore { get; set; }
}
