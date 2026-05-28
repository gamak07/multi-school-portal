namespace MultiPortalSchoolSys.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base("Security Violation: You do not have the required permissions to perform this action.")
    {
    }
}