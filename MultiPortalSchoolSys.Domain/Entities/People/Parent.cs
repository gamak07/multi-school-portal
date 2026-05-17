using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Parent : BaseEntity
{
    // -----------------------------------------------------------------------
    // UserId is now REQUIRED and assigned immediately at account creation.
    // -----------------------------------------------------------------------
    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    // public ApplicationUser? User { get; set; }

    [MaxLength(100)]
    public string? Occupation { get; set; }

    [MaxLength(200)]
    public string? HomeAddress { get; set; }

    public ICollection<Student> Children { get; set; } = new List<Student>();
}
