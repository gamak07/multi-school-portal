namespace MultiPortalSchoolSys.Application.DTOs.Parent;
public class ParentDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Occupation { get; set; }
    public string? HomeAddress { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Children { get; set; } = new();
}
