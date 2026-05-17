namespace MultiPortalSchoolSys.Domain.Common;

/// <summary>
/// Base class for all domain entities.
/// Provides a consistent primary key and audit timestamps
/// across every table in the system.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
