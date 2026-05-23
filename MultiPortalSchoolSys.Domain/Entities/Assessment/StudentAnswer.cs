using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.Assessment;

public class StudentAnswer : BaseEntity
{
    private StudentAnswer() { }
    
    public int StudentId { get; private set; }
    public Student? Student { get; private set; }
    public int ExamId { get; private set; }
    public CbtExam? Exam { get; private set; }
    public int QuestionId { get; private set; }
    public CbtQuestion? Question { get; private set; }

    // Removed [MaxLength(1)]. Can now hold "A", "A,C", or "Photosynthesis"
    public string? SelectedOption { get; private set; } 
    public DateTime SavedAt { get; private set; }

    public StudentAnswer(int studentId, int examId, int questionId, string? selectedOption, CbtQuestionType questionType)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.", nameof(studentId));
        if (examId <= 0) throw new ArgumentException("Invalid exam ID.", nameof(examId));
        if (questionId <= 0) throw new ArgumentException("Invalid question ID.", nameof(questionId));

        StudentId = studentId;
        ExamId = examId;
        QuestionId = questionId;
        
        UpdateAnswer(selectedOption, questionType);
    }

    public void UpdateAnswer(string? selectedOption, CbtQuestionType questionType)
    {
        // 1. Unanswered state handling
        if (string.IsNullOrWhiteSpace(selectedOption))
        {
            SelectedOption = null;
            SavedAt = DateTime.UtcNow;
            return;
        }

        var cleanedInput = selectedOption.Trim();

        // 2. Format verification based on the targeted question type
        if (questionType == CbtQuestionType.SingleChoice)
        {
            var normalized = cleanedInput.ToUpper();
            if (!new[] { "A", "B", "C", "D" }.Contains(normalized))
                throw new ArgumentException("Selection must be A, B, C, or D.");
            SelectedOption = normalized;
        }
        else if (questionType == CbtQuestionType.MultipleChoice)
        {
            var normalizedMulti = cleanedInput.ToUpper().Replace(" ", "");
            var choices = normalizedMulti.Split(',');
            foreach (var choice in choices)
            {
                if (!new[] { "A", "B", "C", "D" }.Contains(choice))
                    throw new ArgumentException($"Invalid multi-select choice: {choice}");
            }
            SelectedOption = normalizedMulti; // Saved as clean comma-separated sequence like "A,C"
        }
        else if (questionType == CbtQuestionType.Structural)
        {
            // Structural accepts any raw string input text typed from the keyboard
            SelectedOption = cleanedInput; 
        }

        SavedAt = DateTime.UtcNow;
    }
}