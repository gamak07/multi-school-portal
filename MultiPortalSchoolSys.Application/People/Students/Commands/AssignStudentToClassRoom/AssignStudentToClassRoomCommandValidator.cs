using FluentValidation;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.AssignStudentToClassRoom;

public sealed class AssignStudentToClassRoomCommandValidator : AbstractValidator<AssignStudentToClassRoomCommand>
{
    public AssignStudentToClassRoomCommandValidator()
    {
        RuleFor(v => v.StudentId)
            .GreaterThan(0)
            .WithMessage("A valid Student ID must be specified.");

        RuleFor(v => v.ClassRoomId)
            .GreaterThan(0)
            .When(v => v.ClassRoomId.HasValue)
            .WithMessage("If specified, the Classroom ID must be a valid positive integer.");
    }
}