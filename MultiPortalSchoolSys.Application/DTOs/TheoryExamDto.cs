namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class TheoryExamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public int Term { get; set; }
    public decimal TotalMarks { get; set; }
    public string ApprovalStatus { get; set; } = string.Empty;
    public string? ApprovalRemarks { get; set; }
    public int QuestionCount { get; set; }
}
