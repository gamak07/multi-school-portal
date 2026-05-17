using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class StudentResult : BaseEntity
{
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public Subject? Subject { get; set; }

    [Required]
    [MaxLength(20)]
    public string AcademicYear { get; set; } = string.Empty;

    // 1 = First Term, 2 = Second Term, 3 = Third Term
    [Required]
    public int Term { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal CAScore { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal ExamScore { get; set; }

    // Always computed by ResultService — never trusted from UI
    [Column(TypeName = "decimal(5,2)")]
    public decimal TotalScore { get; set; }

    [MaxLength(2)]
    public string Grade { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Remark { get; set; } = string.Empty;

    // Only Admin can set this to true — Student/Parent see results only after publish
    public bool IsPublished { get; set; } = false;
}
