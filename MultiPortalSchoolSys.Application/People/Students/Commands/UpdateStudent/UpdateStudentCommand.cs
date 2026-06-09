using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.UpdateStudent;

// 1. THE CONTRACT (AdmissionNo and ParentId are completely removed!)
public record UpdateStudentCommand(
    int Id, 
    DateTime DateOfBirth, 
    int? ClassRoomId) : IRequest<Result<Unit>>;

// 2. THE HANDLER
public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;

    public UpdateStudentCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime)
    {
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
    }

    public async Task<Result<Unit>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var student = await _unitOfWork.Students.GetByIdAsync(request.Id);

            if (student == null)
            {
                return Result<Unit>.Failure("Student record could not be found.", 404);
            }

            // Step 3: Run our tightly encapsulated domain mutations
            student.UpdateCoreDetails(request.DateOfBirth, _dateTime.UtcNow);
            
            student.AssignToClassRoom(request.ClassRoomId);

            // Step 4: Persist tracked memory changes to disk
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (ArgumentException ex)
        {
            return Result<Unit>.Failure(ex.Message, 400);
        }
    }
}