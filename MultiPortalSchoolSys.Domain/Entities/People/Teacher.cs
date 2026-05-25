using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Teacher : BaseEntity
{
    public int UserId { get; private set; }
    public string StaffNo { get; private set; } = string.Empty;
    public DateTime HireDate { get; private set; }
    // public decimal BasicSalary { get; private set; }
    public string? Qualifications { get; private set; }

    private readonly List<Subject> _subjects = [];
    public virtual IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

    private Teacher() { }

    public Teacher(int userId, string staffNo, DateTime hireDate, string? qualifications = null)
    {
        if (userId <= 0) throw new ArgumentException("User ID must be a positive integer.", nameof(userId));
        
        UserId = userId;
        
        UpdateEmploymentDetails(staffNo, hireDate, qualifications);
    }

    public void UpdateEmploymentDetails(string staffNo, DateTime hireDate,  string? qualifications)
    {
        if (string.IsNullOrWhiteSpace(staffNo)) throw new ArgumentException("Staff number cannot be empty.", nameof(staffNo));
        if (hireDate == DateTime.MinValue) throw new ArgumentException("Invalid hire date specification.", nameof(hireDate));

        // Guard Check: Block future-dated hiring anomalies 
        if (hireDate.Date > DateTime.UtcNow.Date)
            throw new ArgumentException("Hire date cannot be set in a future calendar date.", nameof(hireDate));

        StaffNo = staffNo.Trim();
        HireDate = hireDate.Date; // Normalize timestamp boundaries cleanly
        Qualifications = string.IsNullOrWhiteSpace(qualifications) ? null : qualifications.Trim();
    }

    
    public void AssignSubject(Subject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        if (!_subjects.Any(s => s.Id == subject.Id))
        {
            _subjects.Add(subject);
        }
    }

    public void RemoveSubject(Subject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        var existingSubject = _subjects.FirstOrDefault(s => s.Id == subject.Id);
        if (existingSubject != null)
        {
            _subjects.Remove(existingSubject);
        }
    }
}