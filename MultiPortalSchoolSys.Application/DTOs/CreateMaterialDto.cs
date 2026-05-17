using System.ComponentModel.DataAnnotations;
namespace MultiPortalSchoolSys.Application.DTOs.Material;
public class CreateMaterialDto
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string FileUrl { get; set; } = string.Empty;
    [Required] public int SubjectId { get; set; }
    [Required] public int UploadedBy { get; set; }
}
