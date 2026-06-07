namespace MultiPortalSchoolSys.Application.Common.Interfaces;
public interface ICurrentUserService
{
    int UserId { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
    bool IsAdmin => Role == "Admin";
    bool IsTeacher => Role == "Teacher";
    bool IsStudent => Role == "Student";
    bool IsParent => Role == "Parent";
}