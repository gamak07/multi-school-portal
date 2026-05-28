
namespace MultiPortalSchoolSys.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // This is the special constructor our handlers will use most!
    // Example: throw new NotFoundException("TheoryExam", request.Id);
    // Becomes: Entity "TheoryExam" (5) was not found.
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}