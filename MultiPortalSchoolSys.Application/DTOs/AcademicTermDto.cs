namespace MultiPortalSchoolSys.Application.DTOs.Calendar;
public class AcademicTermDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public int TermNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrentTerm { get; set; }
}
