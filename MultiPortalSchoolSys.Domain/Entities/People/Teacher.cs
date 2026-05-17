using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Teacher : BaseEntity
{
    // -----------------------------------------------------------------------
    // UserId is now REQUIRED and assigned immediately at account creation.
    // -----------------------------------------------------------------------
    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    // public ApplicationUser? User { get; set; }

    [Required]
    [MaxLength(20)]
    public string StaffNo { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BasicSalary { get; set; }

    public string? Qualifications { get; set; }

    public ICollection<Academic.Subject> Subjects { get; set; } = new List<Academic.Subject>();
}
