using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class StudentResult : BaseEntity
{

    public int StudentId { get; private set; }
    public Student? Student { get; private set; }

    public int SubjectId { get; private set; }
    public Subject? Subject { get; private set; }


    public int AcademicTermId { get; private set; }
    public AcademicTerm? AcademicTerm { get; private set; }

    public decimal CAScore { get; private set; }
    public decimal ExamScore { get; private set; }
    public decimal TotalScore { get; private set; }

    // Fixed: Changed from public set to private set
    public string Grade { get; private set; } = string.Empty;
    public string Remark { get; private set; } = string.Empty;

    public bool IsPublished { get; private set; } = false;

    // Removed grade, remark, and isPublished from the constructor inputs!
    private StudentResult() { }
    public StudentResult(int studentId, int subjectId, int academicTermId, decimal caScore, decimal examScore, GradingSetting gradingSetting)
    {
        if (studentId <= 0) throw new ArgumentException("Invalid student ID.", nameof(studentId));
        if (subjectId <= 0) throw new ArgumentException("Invalid subject ID.", nameof(subjectId));
        if (academicTermId <= 0) throw new ArgumentException("Invalid academic term ID.", nameof(academicTermId));
        
       
        ArgumentNullException.ThrowIfNull(gradingSetting, nameof(gradingSetting));
        
        StudentId = studentId;
        SubjectId = subjectId;
        AcademicTermId = academicTermId;
        
        UpdateScores(caScore, examScore, gradingSetting);
    }

    // Removed grade, remark, and isPublished from update parameters as well
    public void UpdateScores(decimal caScore, decimal examScore, GradingSetting gradingSetting)
    {
        if (IsPublished) throw new InvalidOperationException("Cannot modify scores once a result has been published.");
        if (caScore < 0 || caScore > gradingSetting.MaxCAScore) throw new ArgumentException($"CA score must be between 0 and {gradingSetting.MaxCAScore}.", nameof(caScore));
        if (examScore < 0 || examScore > gradingSetting.MaxExamScore) throw new ArgumentException($"Exam score must be between 0 and {gradingSetting.MaxExamScore}.", nameof(examScore));
        if ((caScore + examScore) > 100) throw new ArgumentException("Total combined score cannot exceed 100.");

        CAScore = caScore;
        ExamScore = examScore;
        
        CalculateMetrics(gradingSetting);
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
    private void CalculateMetrics(GradingSetting gradingSetting)
    {
        TotalScore = CAScore + ExamScore;

        // Centralized Grading Logic System
        if (TotalScore >= gradingSetting.MinimumA1) { Grade = "A1"; Remark = "Excellent"; }
        else if (TotalScore >= gradingSetting.MinimumB2) { Grade = "B2"; Remark = "Very Good"; }
        else if (TotalScore >= gradingSetting.MinimumB3) { Grade = "B3"; Remark = "Good"; }
        else if (TotalScore >= gradingSetting.MinimumC4) { Grade = "C4"; Remark = "Credit"; }
        else if (TotalScore >= gradingSetting.MinimumC5) { Grade = "C5"; Remark = "Credit"; }
        else if (TotalScore >= gradingSetting.MinimumC6) { Grade = "C6"; Remark = "Credit"; }
        else if (TotalScore >= gradingSetting.MinimumD7) { Grade = "D7"; Remark = "Pass"; }
        else if (TotalScore >= gradingSetting.MinimumE8) { Grade = "E8"; Remark = "Pass"; }
        else { Grade = "F9"; Remark = "Fail"; }
    }
}