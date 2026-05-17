namespace MultiPortalSchoolSys.Application.DTOs.Result;
public class ResultDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public int Term { get; set; }
    public decimal CAScore { get; set; }
    public decimal ExamScore { get; set; }
    public decimal TotalScore { get; set; }
    public string Grade { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
