using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class TheoryQuestionDto
{
    [Required] public string QuestionText { get; set; } = string.Empty;
    public decimal MaxMarks { get; set; }
    public string? ModelAnswer { get; set; }
}
