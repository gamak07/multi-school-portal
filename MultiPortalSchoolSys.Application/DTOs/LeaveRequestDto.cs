namespace MultiPortalSchoolSys.Application.DTOs.Leave;
public class LeaveRequestDto
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string LeaveType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ReviewRemarks { get; set; }
    public DateTime RequestedAt { get; set; }
}
