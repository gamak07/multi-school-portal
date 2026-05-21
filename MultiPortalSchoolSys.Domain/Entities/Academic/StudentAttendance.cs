using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class StudentAttendance : BaseEntity
{

    private StudentAttendance() { }
    public int StudentId { get; private set; }
    // [ForeignKey("StudentId")]
    public Student? Student { get; private set; }

    public int ClassId { get; private set; }
    // [ForeignKey("ClassId")]
    public ClassRoom? ClassRoom { get; private set; }

    // [Required]
    public DateTime Date { get; private set; }

    // [Required]
    // [MaxLength(20)]
    public AttendanceStatus Status { get; private set; }

    public StudentAttendance(int studentId, int classId, DateTime date, AttendanceStatus status)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.");
        if (classId <= 0) throw new ArgumentException("Invalid class ID.");
        if (date == default) throw new ArgumentException("Invalid date.", nameof(date));
       
        StudentId = studentId;
        ClassId = classId;
        Date = date;
        Status = status;
    }

    public void UpdateAttendance(DateTime date, AttendanceStatus status)
    {
        if (date == default) throw new ArgumentException("Invalid date.", nameof(date));
        Date = date;
        Status = status;
    }

}
