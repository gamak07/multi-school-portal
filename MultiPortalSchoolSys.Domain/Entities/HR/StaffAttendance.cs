using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.HR;

public class StaffAttendance : BaseEntity
{
    public int TeacherId { get; private set; }
    // [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; private set; }

    // [Required]
    public DateTime Date { get; private set; }

    // [Required]
    // [MaxLength(20)]
    public AttendanceStatus Status { get; private set; } 

    // [MaxLength(200)]
    public string? Remarks { get; private set; }

    private StaffAttendance() { }

    public StaffAttendance(int teacherId, DateTime date, AttendanceStatus status, string? remarks)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));
        TeacherId = teacherId;
        UpdateAttendance(date, status, remarks);
    }

    public void UpdateAttendance(DateTime date, AttendanceStatus status, string? remarks)
    {
        if (date == DateTime.MinValue) throw new ArgumentException("Invalid attendance date.", nameof(date));
        
        var normalizedDate = date.Date;

        if (normalizedDate > DateTime.UtcNow.Date) 
            throw new ArgumentException("Cannot record or update an attendance log for a future date.", nameof(date));

        Date = normalizedDate;
        Status = status;
        Remarks =string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
    }


}
