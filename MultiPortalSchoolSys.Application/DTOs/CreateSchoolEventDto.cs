using System.ComponentModel.DataAnnotations;
using MultiPortalSchoolSys.Domain.Enums;
namespace MultiPortalSchoolSys.Application.DTOs.Calendar;
public class CreateSchoolEventDto
{
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required] public EventType EventType { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    public bool IsAllDay { get; set; }
    public int? AcademicTermId { get; set; }
    [Required] public string CreatedByAdminId { get; set; } = string.Empty;
}
