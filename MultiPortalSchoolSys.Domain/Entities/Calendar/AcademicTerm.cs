using MultiPortalSchoolSys.Domain.Common;

namespace MultiPortalSchoolSys.Domain.Entities.Calendar;

public class AcademicTerm : BaseEntity
{
    // [Required]
    // [MaxLength(50)]
    public string Name { get; private set; } = string.Empty; // e.g., "2025/2026 Term 1"

    // [Required]
    // [MaxLength(20)]
    public string AcademicYear { get; private set; } = string.Empty; // e.g., "2025/2026"

    public int TermNumber { get; private set; } // 1, 2, or 3

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    // Only one term can be current at a time — enforced by CalendarService
    public bool IsCurrentTerm { get; private set; } = false;

    public ICollection<SchoolEvent> Events { get; private set; } = [];

    private AcademicTerm() { }

    public AcademicTerm(string name, string academicYear, int termNumber, DateTime startDate, DateTime endDate)
    {
        UpdateAcademicTerm(name, academicYear, termNumber, startDate, endDate);
    }

    public void UpdateAcademicTerm(string name, string academicYear, int termNumber, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(academicYear)) throw new ArgumentException("Academic year cannot be empty.", nameof(academicYear));
        if (termNumber < 1 || termNumber > 3) throw new ArgumentException("Term number must be between 1 and 3.", nameof(termNumber));
        if (startDate >= endDate) throw new ArgumentException("Start date must be before end date.");

        Name = name.Trim();
        AcademicYear = academicYear.Trim();
        TermNumber = termNumber;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Activate()
    {
        
        IsCurrentTerm = true;
    }

    public void Deactivate()
    {
        IsCurrentTerm = false;
    }
}
