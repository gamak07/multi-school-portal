namespace MultiPortalSchoolSys.Application.DTOs.Parent;
public class CreateParentDto
{
    public string? Occupation { get; set; }
    public string? HomeAddress { get; set; }
    public string UserId { get; set; } = string.Empty;
}
