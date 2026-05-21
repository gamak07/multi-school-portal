using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class Subject : BaseEntity
{

    private Subject() { }
    // [Required]
    // [MaxLength(100)]
    public string Name { get; private set; } = string.Empty;

    // [Required]
    // [MaxLength(20)]
    public string Code { get; private set; } = string.Empty;

    public bool IsActive { get; private set; } = true;

    public int? ClassId { get; private set; }
    // [ForeignKey("ClassId")]
    public ClassRoom? ClassRoom { get; private set; }

    public int? TeacherId { get; private set; }
    // [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; private set; }

    public Subject(string name, string code, int? classId = null, int? teacherId = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Subject name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Subject code cannot be empty.", nameof(code));

        Name = name;
        Code = code;
        ClassId = classId;
        TeacherId = teacherId;
    }

    public void UpdateSubject(string name, string code, int? classId = null, int? teacherId = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Subject name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Subject code cannot be empty.", nameof(code));

        Name = name;
        Code = code;
        ClassId = classId;
        TeacherId = teacherId;
    }

    public void AssignTeacher(int? teacherId)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));
        TeacherId = teacherId;
    }

    public void UpdateClassroom(int? classId)
    {
        if (classId <= 0) throw new ArgumentException("Invalid class ID.", nameof(classId));
        ClassId = classId;
    }

    public void DeactivateSubject()
    {
        IsActive = false;
    }

    public void ActivateSubject()
    {
        IsActive = true;
    }
}