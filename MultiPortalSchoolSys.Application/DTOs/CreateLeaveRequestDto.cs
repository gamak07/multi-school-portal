using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Leave;
public class CreateLeaveRequestDto
{
    [Required] public int TeacherId { get; set; }
    [Required] public string LeaveType { get; set; } = string.Empty;
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public string Reason { get; set; } = string.Empty;
}
