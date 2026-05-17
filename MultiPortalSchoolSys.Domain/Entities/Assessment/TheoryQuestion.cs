using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class TheoryQuestion : BaseEntity
{
    public int TheoryExamId { get; set; }
    [ForeignKey("TheoryExamId")]
    public TheoryExam? TheoryExam { get; set; }

    [Required]
    public string QuestionText { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5,2)")]
    public decimal MaxMarks { get; set; }

    // Optional marking guide visible to the teacher during grading
    public string? ModelAnswer { get; set; }
}
