using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Student : BaseEntity
{
    // -----------------------------------------------------------------------
    // UserId is now REQUIRED and assigned immediately at account creation.
    // Admin provisions the account first — UserId is never null.
    // UserStatus.PendingActivation replaces null as the activation signal.
    // -----------------------------------------------------------------------
    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    // public ApplicationUser? User { get; set; }

    [Required]
    public int ParentId { get; set; }
    [ForeignKey("ParentId")]
    public Parent? Parent { get; set; }

    public int? ClassRoomId { get; set; }
    [ForeignKey("ClassRoomId")]
    public Academic.ClassRoom? ClassRoom { get; set; }

    [Required]
    [MaxLength(20)]
    public string AdmissionNo { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
}
