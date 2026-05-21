// using MultiPortalSchoolSys.Domain.Common;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

// public class CbtQuestion : BaseEntity
// {
//     public int ExamId { get; set; }
//     [ForeignKey("ExamId")]
//     public CbtExam? Exam { get; set; }

//     [Required]
//     public string QuestionText { get; set; } = string.Empty;

//     [Required]
//     [MaxLength(500)]
//     public string OptionA { get; set; } = string.Empty;

//     [Required]
//     [MaxLength(500)]
//     public string OptionB { get; set; } = string.Empty;

//     [Required]
//     [MaxLength(500)]
//     public string OptionC { get; set; } = string.Empty;

//     [Required]
//     [MaxLength(500)]
//     public string OptionD { get; set; } = string.Empty;

//     [Required]
//     [MaxLength(1)]
//     public string CorrectOption { get; set; } = string.Empty; // A, B, C, or D

//     [Column(TypeName = "decimal(5,2)")]
//     public decimal Marks { get; set; } = 1.0m;
// }

using MultiPortalSchoolSys.Domain.Common;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class CbtQuestion : BaseEntity
{
    public int ExamId { get; private set; }
    public CbtExam? Exam { get; private set; }

    public string QuestionText { get; private set; } = string.Empty;
    public string OptionA { get; private set; } = string.Empty;
    public string OptionB { get; private set; } = string.Empty;
    public string OptionC { get; private set; } = string.Empty;
    public string OptionD { get; private set; } = string.Empty;
    public string CorrectOption { get; private set; } = string.Empty;
    public decimal Marks { get; private set; }

    // EF Core requires a parameterless constructor for materialization
    private CbtQuestion() { }

    public CbtQuestion(int examId, string questionText, string a, string b, string c, string d, string correctOption, decimal marks)
    {
        if (examId <= 0) throw new ArgumentException("Invalid exam ID.", nameof(examId));
        ExamId = examId;
        UpdateDetails(questionText, a, b, c, d, correctOption, marks);
    }

    public void UpdateDetails(string questionText, string a, string b, string c, string d, string correctOption, decimal marks)
    {
        if (string.IsNullOrWhiteSpace(questionText)) throw new ArgumentException("Question text cannot be empty.");
        if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b) || string.IsNullOrWhiteSpace(c) || string.IsNullOrWhiteSpace(d))
            throw new ArgumentException("All options must be provided and cannot be empty.");
        if (!new[] { "A", "B", "C", "D" }.Contains(correctOption.ToUpper())) throw new ArgumentException("Invalid correct option.");
        if (marks <= 0) throw new ArgumentException("Marks must be greater than zero.");

        QuestionText = questionText;
        OptionA = a;
        OptionB = b;
        OptionC = c;
        OptionD = d;
        CorrectOption = correctOption.ToUpper();
        Marks = marks;
    }
}