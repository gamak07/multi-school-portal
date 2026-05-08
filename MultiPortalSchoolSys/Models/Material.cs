using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string FileUrl { get; set; } = string.Empty; // Path to the uploaded PDF/Doc

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }

        public int UploadedBy { get; set; }
        [ForeignKey("UploadedBy")]
        public Teacher? Teacher { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}