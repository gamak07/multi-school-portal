namespace MultiPortalSchoolSys.Application.DTOs.Exam;
public class PrintableExamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string DocumentUrl { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public int Term { get; set; }
    public string ApprovalStatus { get; set; } = string.Empty;
    public string? ApprovalRemarks { get; set; }
}
