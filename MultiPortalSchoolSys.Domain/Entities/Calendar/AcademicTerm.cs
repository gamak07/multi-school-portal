using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MultiPortalSchoolSys.Domain.Entities.Calendar;

public class AcademicTerm : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty; // e.g., "2025/2026 Term 1"

    [Required]
    [MaxLength(20)]
    public string AcademicYear { get; set; } = string.Empty; // e.g., "2025/2026"

    public int TermNumber { get; set; } // 1, 2, or 3

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Only one term can be current at a time — enforced by CalendarService
    public bool IsCurrentTerm { get; set; } = false;

    public ICollection<SchoolEvent> Events { get; set; } = new List<SchoolEvent>();
}
