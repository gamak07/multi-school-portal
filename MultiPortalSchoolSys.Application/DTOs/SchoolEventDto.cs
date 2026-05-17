namespace MultiPortalSchoolSys.Application.DTOs.Calendar;
public class SchoolEventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAllDay { get; set; }
    public int? AcademicTermId { get; set; }
}
