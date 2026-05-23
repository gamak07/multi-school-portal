using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class CbtQuestion : BaseEntity
{
    public int ExamId { get; private set; }
    public CbtExam? Exam { get; private set; }
    public CbtQuestionType QuestionType { get; private set; }
    public string QuestionText { get; private set; } = string.Empty;
    public string? OptionA { get; private set; }
    public string? OptionB { get; private set; }
    public string? OptionC { get; private set; }
    public string? OptionD { get; private set; }
    public string CorrectOption { get; private set; } = string.Empty;
    public decimal Marks { get; private set; }

    private CbtQuestion() { }

    public CbtQuestion(int examId, CbtQuestionType questionType, string questionText, string? a, string? b, string? c, string? d, string correctOption, decimal marks)
    {
        if (examId <= 0) throw new ArgumentException("Invalid exam ID.", nameof(examId));
        ExamId = examId;
        UpdateDetails(questionType, questionText, a, b, c, d, correctOption, marks);
    }

    // FIXED: Parameter types updated to string? to match the constructor and prevent compiler errors
    public void UpdateDetails(CbtQuestionType questionType, string questionText, string? a, string? b, string? c, string? d, string correctOption, decimal marks)
    {
        if (string.IsNullOrWhiteSpace(questionText)) throw new ArgumentException("Question text cannot be empty.", nameof(questionText));
        if (string.IsNullOrWhiteSpace(correctOption)) throw new ArgumentException("Correct option/answer cannot be empty.", nameof(correctOption));
        if (marks <= 0) throw new ArgumentException("Marks must be greater than zero.", nameof(marks));

        // Format-specific evaluation routing
        if (questionType == CbtQuestionType.Structural)
        {
            // Cleanly isolate fill-in-the-gap questions: options are wiped out safely
            OptionA = null;
            OptionB = null;
            OptionC = null;
            OptionD = null;
            CorrectOption = correctOption.Trim();
        }
        else
        {
            // MCQ and Checkbox formats strictly demand all options are provided
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b) || string.IsNullOrWhiteSpace(c) || string.IsNullOrWhiteSpace(d))
                throw new ArgumentException("All options (A, B, C, D) must be provided for multiple choice formats.");

            OptionA = a.Trim();
            OptionB = b.Trim();
            OptionC = c.Trim();
            OptionD = d.Trim();

            var normalizedAnswer = correctOption.Trim().ToUpper().Replace(" ", "");

            if (questionType == CbtQuestionType.SingleChoice)
            {
                if (!new[] { "A", "B", "C", "D" }.Contains(normalizedAnswer)) 
                    throw new ArgumentException("Invalid correct option for single choice. Must be A, B, C, or D.", nameof(correctOption));
            }
            else if (questionType == CbtQuestionType.MultipleChoice)
            {
                // Validate that all tokens inside a multi-select checkbox string (e.g., "A,C") are authorized options
                var selections = normalizedAnswer.Split(',');
                foreach (var selection in selections)
                {
                    if (!new[] { "A", "B", "C", "D" }.Contains(selection))
                        throw new ArgumentException($"Invalid selection option '{selection}' found in multi-choice answer list.", nameof(correctOption));
                }
            }

            CorrectOption = normalizedAnswer;
        }

        QuestionType = questionType;
        Marks = marks;
    }
}