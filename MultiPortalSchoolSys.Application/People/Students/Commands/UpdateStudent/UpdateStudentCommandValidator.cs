using FluentValidation;
using MultiPortalSchoolSys.Application.Common.Interfaces;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.UpdateStudent;

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator(IDateTime dateTime)
    {
        RuleFor(v => v.Id).GreaterThan(0)
            .WithMessage("A valid Student ID must be specified for update operations.");

        RuleFor(v => v.DateOfBirth).NotEmpty()
            .WithMessage("Date of Birth must be specified.")
            .LessThan(v => dateTime.UtcNow)
            .WithMessage("Date of Birth cannot be a date in the future.");

        RuleFor(v => v.ClassRoomId)
            .GreaterThan(0)
            .When(v => v.ClassRoomId.HasValue)
            .WithMessage("If specified, the Classroom ID must be a valid positive integer.");
    }
}