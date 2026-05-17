using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Attendance;
public class TakeAttendanceDto
{
    [Required] public int ClassRoomId { get; set; }
    [Required] public DateOnly Date { get; set; }
    [Required] public List<StudentAttendanceEntryDto> Entries { get; set; } = new();
}
public class StudentAttendanceEntryDto
{
    public int StudentId { get; set; }
    public string Status { get; set; } = string.Empty; // Present, Absent, Late
}
