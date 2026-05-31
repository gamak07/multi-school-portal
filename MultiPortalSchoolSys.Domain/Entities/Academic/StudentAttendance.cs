using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class StudentAttendance : BaseEntity
{

    public int StudentId { get; private set; }
    public Student? Student { get; private set; }

    public int ClassId { get; private set; }
    public ClassRoom? ClassRoom { get; private set; }
    public DateTime Date { get; private set; }
    public AttendanceStatus Status { get; private set; }
    private StudentAttendance() { }
    public StudentAttendance(int studentId, int classId, DateTime date, AttendanceStatus status)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.");
        if (classId <= 0) throw new ArgumentException("Invalid class ID.");

        StudentId = studentId;
        ClassId = classId;
        UpdateAttendance(date, status);
    }

    public void UpdateAttendance(DateTime date, AttendanceStatus status)
    {
        if (date == default || date == DateTime.MinValue)
            throw new ArgumentException("Invalid attendance date specification.", nameof(date));

        var normalizedDate = date.Date;

        if (normalizedDate > DateTime.UtcNow.Date)
            throw new ArgumentException("Cannot record student attendance logs for a future calendar date.");
        Date = normalizedDate;
        Status = status;
    }

}
