using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Calendar;

public class SchoolEvent : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public EventType EventType { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsAllDay { get; set; } = false;

    // Optional — holidays may span or fall outside a term
    public int? AcademicTermId { get; set; }
    [ForeignKey("AcademicTermId")]
    public AcademicTerm? AcademicTerm { get; set; }

    [Required]
    public string CreatedByAdminId { get; set; } = string.Empty;
    // [ForeignKey("CreatedByAdminId")]
    // public ApplicationUser? CreatedByAdmin { get; set; }
}
