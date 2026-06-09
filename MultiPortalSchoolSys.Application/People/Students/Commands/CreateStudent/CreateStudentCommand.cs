using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;
using MultiPortalSchoolSys.Domain.Entities.People;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.CreateStudent;

// 1. THE COMMAND (The Input Data Contract)
public record CreateStudentCommand(
    int UserId,
    int ParentId,
    int? ClassRoomId,
    string AdmissionNo,
    DateTime DateOfBirth,
    DateTime EnrollmentDate) : IRequest<Result<int>>;

// 2. THE HANDLER (The Execution Engine)
public sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Step A: Instantiate the Domain Entity using its defensive business rules
            var student = new Student(
                request.UserId,
                request.ParentId,
                request.AdmissionNo,
                request.DateOfBirth,
                request.EnrollmentDate
            );

            if (request.ClassRoomId.HasValue)
            {
                student.AssignToClassRoom(request.ClassRoomId.Value);
            }

            // Step B: Stage the entity into the Unit of Work tracking context
            await _unitOfWork.Students.AddAsync(student, cancellationToken);

            // Step C: Explicitly save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Step D: Return the newly generated tracking ID inside our success wrapper
            return Result<int>.Success(student.Id);
        }
        catch (ArgumentException ex)
        {
            // Catch intentional validation guard failures thrown by the Domain rules
            return Result<int>.Failure(ex.Message, 400);
        }
    }
}