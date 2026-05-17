using System.ComponentModel.DataAnnotations;
using MultiPortalSchoolSys.Domain.Enums;
namespace MultiPortalSchoolSys.Application.DTOs.Sanction;
public class CreateSanctionDto
{
    [Required] public string IssuedToUserId { get; set; } = string.Empty;
    [Required] public SanctionType SanctionType { get; set; }
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public string IssuedByAdminId { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
}
