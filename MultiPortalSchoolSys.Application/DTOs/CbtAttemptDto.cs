namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class CbtAttemptDto
{
    public int AttemptId { get; set; }
    public int ExamId { get; set; }
    public string ExamTitle { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<CbtQuestionDto> Questions { get; set; } = [];
}
