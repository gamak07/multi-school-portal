using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class ClassRoom : BaseEntity
{

    private ClassRoom() { }

    public string Name { get; private set; } = string.Empty;

    public string Arm { get; private set; } = string.Empty;

    public int? FormTeacherId { get; private set; }

    public Teacher? FormTeacher { get; private set; }

    private readonly List<Student> _students = [];
    public virtual IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    private readonly List<Subject> _subjects = [];
    public virtual IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();


    public ClassRoom(string name, string arm, int? formTeacherId = null)
    {
        if (formTeacherId.HasValue && formTeacherId.Value <= 0)
            throw new ArgumentException("Form Teacher ID must be a positive integer.", nameof(formTeacherId));

        FormTeacherId = formTeacherId;
        UpdateClassDetails(name, arm);
    }

    public void UpdateClassDetails(string name, string arm)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Class name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(arm)) throw new ArgumentException("Class arm cannot be empty.", nameof(arm));

        Name = name.Trim();
        Arm = arm.Trim().ToUpper();
    }

    public void AssignFormTeacher(int teacherId)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));
        FormTeacherId = teacherId;
    }

    // Explicit domain verbs for roster movements
    public void EnrollStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (!_students.Any(s => s.Id == student.Id))
        {
            _students.Add(student);
        }
    }

    public void AllocateSubject(Subject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);
        if (!_subjects.Any(s => s.Id == subject.Id))
        {
            _subjects.Add(subject);
            if (subject.ClassId != Id)
            {
                subject.UpdateClassroom(Id);
            }
        }
    }
}