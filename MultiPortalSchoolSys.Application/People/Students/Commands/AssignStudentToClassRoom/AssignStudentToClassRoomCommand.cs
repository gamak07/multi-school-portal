using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.AssignStudentToClassRoom;

// 1. THE CONTRACT
// Changing ClassRoomId to int? allows this single command to handle both transitions and withdrawals!
public record AssignStudentToClassRoomCommand(int StudentId, int? ClassRoomId) : IRequest<Result<Unit>>;

// 2. THE HANDLER
public sealed class AssignStudentToClassRoomCommandHandler : IRequestHandler<AssignStudentToClassRoomCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignStudentToClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(AssignStudentToClassRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Step 1: Fetch and check the student record
            var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                return Result<Unit>.Failure("Student record could not be found.", 404);
            }

            // Step 2: Fetch and check the destination classroom record (only if an ID is actually provided)
            if (request.ClassRoomId.HasValue)
            {
                var classRoom = await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoomId.Value);
                if (classRoom == null)
                {
                    return Result<Unit>.Failure("The designated Classroom record could not be found.", 404);
                }
            }
            
            // Step 3: Trigger the domain entity assignment method safely inside our execution trap
            student.AssignToClassRoom(request.ClassRoomId);
            
            // Step 4: Commit transaction via Unit of Work
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            // Step 5: Return clean Success wrapper
            return Result<Unit>.Success(Unit.Value);
        }
        catch (ArgumentException ex)
        {
            // Gracefully catch structural domain guard violations and translate them into a 400 Bad Request
            return Result<Unit>.Failure(ex.Message, 400);
        }
    }
}