namespace MultiPortalSchoolSys.Application.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException()
        : base("A data conflict or duplication constraint has occurred in the system.")
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }


}