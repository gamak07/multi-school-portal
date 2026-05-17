using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class Subject : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int? ClassId { get; set; }
    [ForeignKey("ClassId")]
    public ClassRoom? ClassRoom { get; set; }

    public int? TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; set; }
}
