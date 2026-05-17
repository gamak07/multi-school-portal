namespace MultiPortalSchoolSys.Application.DTOs.Material;
public class MaterialDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
