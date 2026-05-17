namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class ExamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string CreatedByTeacher { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TotalMarks { get; set; }
    public string ApprovalStatus { get; set; } = string.Empty;
    public string? ApprovalRemarks { get; set; }
    public bool IsActive { get; set; }
    public int QuestionCount { get; set; }
}
