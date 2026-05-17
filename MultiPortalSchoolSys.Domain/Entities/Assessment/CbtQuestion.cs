using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class CbtQuestion : BaseEntity
{
    public int ExamId { get; set; }
    [ForeignKey("ExamId")]
    public CbtExam? Exam { get; set; }

    [Required]
    public string QuestionText { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string OptionA { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string OptionB { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string OptionC { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string OptionD { get; set; } = string.Empty;

    [Required]
    [MaxLength(1)]
    public string CorrectOption { get; set; } = string.Empty; // A, B, C, or D

    [Column(TypeName = "decimal(5,2)")]
    public decimal Marks { get; set; } = 1.0m;
}
