using MediatR;
using MultiPortalSchoolSys.Application.Common.Models;
using MultiPortalSchoolSys.Application.Common.Interfaces;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.DeleteStudent;

// 1. THE CONTRACT
public record DeleteStudentCommand(int Id) : IRequest<Result<Unit>>;

// 2. THE HANDLER
public sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Fetch the existing student from the repository
        var student = await _unitOfWork.Students.GetByIdAsync(request.Id);

        // Step 2: Guard check against missing data entries
        if (student == null)
        {
            return Result<Unit>.Failure("Student record could not be found.", 404);
        }

        // Step 3: Flag the tracked entity state as Deleted in system memory 
        _unitOfWork.Students.Remove(student);

        // Step 4: Dispatch changes out to the physical SQL database transaction pipeline
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Step 5: Return a clean confirmation wrapper
        return Result<Unit>.Success(Unit.Value);
    }
}