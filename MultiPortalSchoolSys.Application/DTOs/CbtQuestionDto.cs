using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class CbtQuestionDto
{
    [Required] public string QuestionText { get; set; } = string.Empty;
    [Required] public string OptionA { get; set; } = string.Empty;
    [Required] public string OptionB { get; set; } = string.Empty;
    [Required] public string OptionC { get; set; } = string.Empty;
    [Required] public string OptionD { get; set; } = string.Empty;
    [Required][MaxLength(1)] public string CorrectOption { get; set; } = string.Empty;
    public decimal Marks { get; set; } = 1.0m;
}
