using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.HR;

public class Sanction : BaseEntity
{
    public int IssuedByAdminId { get; private set; }
    public int? StudentId { get; private set; }
    public Student? Student { get; private set; }
    public int? TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }
    public SanctionType SanctionType { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime IssuedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; private set; }
    public bool IsResolved { get; private set; } = false;
    public DateTime? ResolvedAt { get; private set; }
    public string TargetRole => StudentId.HasValue ? "Student" : "Teacher";
    private Sanction() { }

    public Sanction(int issuedByAdminId, int? studentId, int? teacherId, SanctionType sanctionType, string description, DateTime? expiresAt = null)
    {
        if (issuedByAdminId <= 0) throw new ArgumentException("Invalid admin ID.", nameof(issuedByAdminId));

        if (studentId.HasValue == teacherId.HasValue)
            throw new ArgumentException("A sanction must be assigned to either a Student or a Teacher, but cannot be assigned to both or neither.");

        if (studentId.HasValue && studentId.Value <= 0) throw new ArgumentException("Invalid student ID target.", nameof(studentId));
        if (teacherId.HasValue && teacherId.Value <= 0) throw new ArgumentException("Invalid teacher ID target.", nameof(teacherId));

        // 2. Strict Enum Rule Guard: Restrict Expulsions completely to Student entities
        if (sanctionType == SanctionType.Expulsion && !studentId.HasValue)
            throw new InvalidOperationException("Critical Violation: Expulsion sanctions can strictly be targeted towards students only.");

        IssuedByAdminId = issuedByAdminId;
        StudentId = studentId;
        TeacherId = teacherId;
        SanctionType = sanctionType;

        UpdateDetails(description, expiresAt);
    }

    public void UpdateDetails(string description, DateTime? expiresAt)
    {
        if (IsResolved)
            throw new InvalidOperationException("Cannot modify a disciplinary action that has already been resolved.");

        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Sanction case notes and description cannot be empty.", nameof(description));

        // Guard Check: Verify future date sequencing for expirations
        if (expiresAt.HasValue && expiresAt.Value <= IssuedAt)
            throw new ArgumentException("Sanction expiration date must occur after the official issuance timestamp.", nameof(expiresAt));

        Description = description.Trim();
        ExpiresAt = expiresAt;
    }

    public void Resolve()
    {
        if (IsResolved)
            throw new InvalidOperationException("This disciplinary action has already been closed and resolved.");

        IsResolved = true;
        ResolvedAt = DateTime.UtcNow;
    }
}