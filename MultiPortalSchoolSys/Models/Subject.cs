using System.ComponentModel.DataAnnotations;

namespace MultiPortalSchoolSys.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // e.g., "Mathematics"

        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty; // e.g., "MTH101"

        public bool IsActive { get; set; } = true;
    }
}