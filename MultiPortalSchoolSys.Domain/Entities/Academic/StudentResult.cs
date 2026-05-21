using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class StudentResult : BaseEntity
{
    private StudentResult() { }

    public int StudentId { get; private set; }
    public Student? Student { get; private set; }

    public int SubjectId { get; private set; }
    public Subject? Subject { get; private set; }

    public string AcademicYear { get; private set; } = string.Empty;

    // 1 = First Term, 2 = Second Term, 3 = Third Term
    public int Term { get; private set; }

    public decimal CAScore { get; private set; }
    public decimal ExamScore { get; private set; }
    public decimal TotalScore { get; private set; }

    // Fixed: Changed from public set to private set
    public string Grade { get; private set; } = string.Empty;
    public string Remark { get; private set; } = string.Empty;

    public bool IsPublished { get; private set; } = false;

    // Removed grade, remark, and isPublished from the constructor inputs!
    public StudentResult(int studentId, int subjectId, string academicYear, int term, decimal caScore, decimal examScore)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.", nameof(studentId));
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (string.IsNullOrWhiteSpace(academicYear)) throw new ArgumentException("Academic year cannot be empty.", nameof(academicYear));
        if (term < 1 || term > 3) throw new ArgumentException("Term must be between 1 and 3.", nameof(term));
        
        // Assuming standard max continuous assessment is 30 or 40, adjust boundaries here if needed. 
        // If 100 is your total absolute max for individual components:
        if (caScore < 0 || caScore > 30) throw new ArgumentException("CA score must be between 0 and 30.", nameof(caScore));
        if (examScore < 0 || examScore > 70) throw new ArgumentException("Exam score must be between 0 and 70.", nameof(examScore));
        if ((caScore + examScore) > 100) throw new ArgumentException("Total combined score cannot exceed 100.");

        StudentId = studentId;
        SubjectId = subjectId;
        AcademicYear = academicYear;
        Term = term;
        CAScore = caScore;
        ExamScore = examScore;
        
        // Calculate business logic internally!
        CalculateMetrics();
    }

    // Removed grade, remark, and isPublished from update parameters as well
    public void UpdateScores(decimal caScore, decimal examScore)
    {
        if (IsPublished) throw new InvalidOperationException("Cannot modify scores once a result has been published.");
        if (caScore < 0 || caScore > 30) throw new ArgumentException("CA score must be between 0 and 30.", nameof(caScore));
        if (examScore < 0 || examScore > 70) throw new ArgumentException("Exam score must be between 0 and 70.", nameof(examScore));
        if ((caScore + examScore) > 100) throw new ArgumentException("Total combined score cannot exceed 100.");

        CAScore = caScore;
        ExamScore = examScore;
        
        CalculateMetrics();
    }

    // Explicit Admin action
    public void Publish()
    {
        IsPublished = true;
    }

    // Explicit Admin action to unpublish if a dispute occurs
    public void Unpublish()
    {
        IsPublished = false;
    }

    // Private helper method to centralize grading rules (Single Source of Truth)
    private void CalculateMetrics()
    {
        TotalScore = CAScore + ExamScore;

        // Centralized Grading Logic System
        if (TotalScore >= 75) { Grade = "A1"; Remark = "Excellent"; }
        else if (TotalScore >= 70) { Grade = "B2"; Remark = "Very Good"; }
        else if (TotalScore >= 65) { Grade = "B3"; Remark = "Good"; }
        else if (TotalScore >= 60) { Grade = "C4"; Remark = "Credit"; }
        else if (TotalScore >= 55) { Grade = "C5"; Remark = "Credit"; }
        else if (TotalScore >= 50) { Grade = "C6"; Remark = "Credit"; }
        else if (TotalScore >= 45) { Grade = "D7"; Remark = "Pass"; }
        else if (TotalScore >= 40) { Grade = "E8"; Remark = "Pass"; }
        else { Grade = "F9"; Remark = "Fail"; }
    }
}