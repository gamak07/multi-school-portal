using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Content;

public class Material : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string FileUrl { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public Subject? Subject { get; set; }

    public int UploadedBy { get; set; }
    [ForeignKey("UploadedBy")]
    public Teacher? Teacher { get; set; }
}
