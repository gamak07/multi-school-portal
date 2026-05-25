using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Calendar;

public class SchoolEvent : BaseEntity
{
    // [Required]
    // [MaxLength(200)]
    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public EventType EventType { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public bool IsAllDay { get; private set; } = false;

    // Optional — holidays may span or fall outside a term
    public int? AcademicTermId { get; private set; }
    // [ForeignKey("AcademicTermId")]
    public AcademicTerm? AcademicTerm { get; private set; }

    // [Required]
    public int CreatedByAdminId { get; private set; }
    // [ForeignKey("CreatedByAdminId")]
    // public ApplicationUser? CreatedByAdmin { get; private set; }

    private SchoolEvent() { }

    public SchoolEvent(string title, EventType eventType, DateTime startDate, DateTime endDate, bool isAllDay, int createdByAdminId, int? academicTermId = null, string? description = null)
    {
        if (createdByAdminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(createdByAdminId));
        if (academicTermId.HasValue && academicTermId <= 0) throw new ArgumentException("Invalid academic term ID.", nameof(academicTermId));
        UpdateEvent(title, eventType, startDate, endDate, isAllDay, description);
        CreatedByAdminId = createdByAdminId;
        AcademicTermId = academicTermId;
    }

    public void UpdateEvent(string title, EventType eventType, DateTime startDate, DateTime endDate, bool isAllDay, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (startDate >= endDate) throw new ArgumentException("Start date must be before end date.");

        Title = title.Trim();
        EventType = eventType;
        StartDate = startDate;
        EndDate = endDate;
        IsAllDay = isAllDay;
        Description = description?.Trim();
    }
}
