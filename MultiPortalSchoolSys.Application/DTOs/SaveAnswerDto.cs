using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class SaveAnswerDto
{
    [Required] public int AttemptId { get; set; }
    [Required] public int StudentId { get; set; }
    [Required] public int ExamId { get; set; }
    [Required] public int QuestionId { get; set; }
    [MaxLength(1)] public string? SelectedOption { get; set; }
}
