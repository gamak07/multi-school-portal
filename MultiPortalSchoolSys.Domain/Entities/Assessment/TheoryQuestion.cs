using MultiPortalSchoolSys.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class TheoryQuestion : BaseEntity
{
    public int TheoryExamId { get; private set; }
    [ForeignKey("TheoryExamId")]
    public TheoryExam? TheoryExam { get; private set; }

    [Required]
    public string QuestionText { get; private set; } = string.Empty;

    [Column(TypeName = "decimal(5,2)")]
    public decimal MaxMarks { get; private set; }

    // Optional marking guide visible to the teacher during grading
    public string? ModelAnswer { get; private set; }

    private TheoryQuestion() { }

    public TheoryQuestion(int theoryExamId, string questionText, decimal maxMarks, string? modelAnswer = null)
    {
        if (theoryExamId <= 0) throw new ArgumentException("Invalid theory exam ID.", nameof(theoryExamId));
        TheoryExamId = theoryExamId;
        UpdateTheoryQuestion(questionText, maxMarks, modelAnswer);
    }

    public void UpdateTheoryQuestion(string questionText, decimal maxMarks, string? modelAnswer = null)
    {
        if (string.IsNullOrWhiteSpace(questionText)) throw new ArgumentException("Question text cannot be empty.", nameof(questionText));
        if (maxMarks <= 0) throw new ArgumentException("Max marks must be greater than zero.", nameof(maxMarks));

        QuestionText = questionText.Trim();
        MaxMarks = maxMarks;
        ModelAnswer = modelAnswer?.Trim();
    }
}
