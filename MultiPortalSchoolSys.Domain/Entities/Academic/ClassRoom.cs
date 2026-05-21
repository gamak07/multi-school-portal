using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class ClassRoom : BaseEntity
{

    private ClassRoom() { }

    public string Name { get; private set; } = string.Empty;

    public string Arm { get; private set; } = string.Empty;

    public int? FormTeacherId { get; private set; }

    public Teacher? FormTeacher { get; private set; }

    // public ICollection<Student> Students { get; private set; } = new List<Student>();
    public ICollection<Student> Students { get; private set; } = [];
    // public ICollection<Subject> Subjects { get; private set; } = new List<Subject>();
    public ICollection<Subject> Subjects { get; private set; } = [];


    public ClassRoom(string name, string arm, int? formTeacherId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Class name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(arm)) throw new ArgumentException("Class arm cannot be empty.", nameof(arm));

        Name = name;
        Arm = arm;
        FormTeacherId = formTeacherId;
    }

    public void AssignFormTeacher(int teacherId)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));
        FormTeacherId = teacherId;
    }

    public void UpdateClassDetails(string name, string arm)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Class name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(arm)) throw new ArgumentException("Class arm cannot be empty.", nameof(arm));

        Name = name;
        Arm = arm;
    }
}