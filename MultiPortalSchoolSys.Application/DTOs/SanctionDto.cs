namespace MultiPortalSchoolSys.Application.DTOs.Sanction;
public class SanctionDto
{
    public int Id { get; set; }
    public string IssuedToUserId { get; set; } = string.Empty;
    public string IssuedToName { get; set; } = string.Empty;
    public string SanctionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IssuedByAdminId { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsResolved { get; set; }
}
