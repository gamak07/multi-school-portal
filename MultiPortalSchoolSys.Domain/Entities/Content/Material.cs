using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Content;

public class Material : BaseEntity
{
    // [Required]
    // [MaxLength(200)]
    public string Title { get; private set; } = string.Empty;

    // [Required]
    public string FileUrl { get; private set; } = string.Empty;

    public int SubjectId { get; private set; }
    // [ForeignKey("SubjectId")]
    public Subject? Subject { get; private set; }

    public int TeacherId { get; private set; }
    // [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; private set; }

    private Material() { }

    public Material(string title, string fileUrl, int subjectId, int teacherId)
    {
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));

        SubjectId = subjectId;
        TeacherId = teacherId;

        UpdateDetails(title, fileUrl);
    }

    public void UpdateDetails(string title, string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(fileUrl)) throw new ArgumentException("File URL cannot be empty.", nameof(fileUrl));

        Title = title.Trim();
        FileUrl = fileUrl.Trim();
    }
}
