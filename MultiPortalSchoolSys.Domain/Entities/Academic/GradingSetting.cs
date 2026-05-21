using MultiPortalSchoolSys.Domain.Common;

namespace MultiPortalSchoolSys.Domain.Entities.Academic;

public class GradingSetting : BaseEntity
{
    private GradingSetting() { }

    public decimal MaxCAScore { get; private set; }
    public decimal MaxExamScore { get; private set; }
    
    public decimal MinimumA1 { get; private set; }
    public decimal MinimumB2 { get; private set; }
    public decimal MinimumB3 { get; private set; }
    public decimal MinimumC4 { get; private set; }
    public decimal MinimumC5 { get; private set; }
    public decimal MinimumC6 { get; private set; }
    public decimal MinimumD7 { get; private set; }
    public decimal MinimumE8 { get; private set; }

    public GradingSetting(decimal maxCAScore, decimal maxExamScore, decimal minimumA1, decimal minimumB2, decimal minimumB3, decimal minimumC4, decimal minimumC5, decimal minimumC6, decimal minimumD7, decimal minimumE8)
    {
        UpdateGradingSetting(maxCAScore, maxExamScore, minimumA1, minimumB2, minimumB3, minimumC4, minimumC5, minimumC6, minimumD7, minimumE8);
    }

    public void UpdateGradingSetting(decimal maxCAScore, decimal maxExamScore, decimal minimumA1, decimal minimumB2, decimal minimumB3, decimal minimumC4, decimal minimumC5, decimal minimumC6, decimal minimumD7, decimal minimumE8)
    {
        // 1. Structural score allocation check
        if (maxCAScore + maxExamScore != 100) 
            throw new ArgumentException("Total maximum score must equal exactly 100.");

        // 2. Defensive check to prevent backwards grading scale ranges
        if (minimumA1 <= minimumB2 || minimumB2 <= minimumB3 || minimumB3 <= minimumC4 || 
            minimumC4 <= minimumC5 || minimumC5 <= minimumC6 || minimumC6 <= minimumD7 || 
            minimumD7 <= minimumE8)
        {
            throw new ArgumentException("Invalid grading scale hierarchy. Higher grades must have higher score thresholds.");
        }

        // 3. Score sanity checks
        if (maxCAScore < 0 || maxExamScore < 0 || minimumE8 < 0 || minimumA1 > 100)
            throw new ArgumentException("Scores and thresholds must be valid positive values up to 100.");

        MaxCAScore = maxCAScore;
        MaxExamScore = maxExamScore;
        MinimumA1 = minimumA1;
        MinimumB2 = minimumB2;
        MinimumB3 = minimumB3;  
        MinimumC4 = minimumC4;
        MinimumC5 = minimumC5;  
        MinimumC6 = minimumC6;
        MinimumD7 = minimumD7;
        MinimumE8 = minimumE8;
    }
}