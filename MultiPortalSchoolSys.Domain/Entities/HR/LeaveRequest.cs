using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;

namespace MultiPortalSchoolSys.Domain.Entities.HR;

public class LeaveRequest : BaseEntity
{
    public int TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }

    public string LeaveType { get; private set; } = string.Empty; // e.g., "Annual", "Sick", "Maternity"
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Reason { get; private set; } = string.Empty;

    public LeaveStatus Status { get; private set; } = LeaveStatus.Pending;

    public int? ReviewedByAdminId { get; private set; }
    public string? ReviewRemarks { get; private set; }
    public DateTime RequestedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? ActionedAt { get; private set; }

    private LeaveRequest() { }

    public LeaveRequest(int teacherId, string leaveType, DateTime startDate, DateTime endDate, string reason)
    {
        if (teacherId <= 0) throw new ArgumentException("Invalid teacher ID.", nameof(teacherId));

        TeacherId = teacherId;

        UpdateDetails(leaveType, startDate, endDate, reason);
    }

    public void UpdateDetails(string leaveType, DateTime startDate, DateTime endDate, string reason)
    {
        if (Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Cannot modify a leave request that has already been actioned by HR.");

        if (string.IsNullOrWhiteSpace(leaveType)) throw new ArgumentException("Leave type must be specified.", nameof(leaveType));
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Reason for leave cannot be empty.", nameof(reason));
        if (startDate >= endDate) throw new ArgumentException("Start date must occur before the end date.");

        LeaveType = leaveType.Trim();
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason.Trim();
    }

    public void Approve(int adminId, string? remarks = null)
    {
        EnsurePendingReview();
        if (adminId <= 0) throw new ArgumentException("Invalid Admin ID.", nameof(adminId));

        Status = LeaveStatus.Approved;
        ReviewedByAdminId = adminId;
        ActionedAt = DateTime.UtcNow;
        ReviewRemarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim();
    }

    public void Reject(int adminId, string remarks)
    {
        EnsurePendingReview();
        if (adminId <= 0) throw new ArgumentException("Invalid Admin ID.", nameof(adminId));
        if (string.IsNullOrWhiteSpace(remarks)) throw new ArgumentException("You must provide feedback remarks explaining why the request was rejected.", nameof(remarks));

        Status = LeaveStatus.Rejected;
        ReviewedByAdminId = adminId;
        ActionedAt = DateTime.UtcNow;
        ReviewRemarks = remarks.Trim();
    }

    private void EnsurePendingReview()
    {
        if (Status != LeaveStatus.Pending)
            throw new InvalidOperationException($"This leave request cannot be processed because it is already marked as {Status}.");
    }
}
