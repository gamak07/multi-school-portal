using FluentValidation;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.DeleteStudent;

// 3. THE VALIDATOR (The Automated Security Checkpoint)
public sealed class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        // Enforce that the identifier payload is structurally sound before handling
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("A valid Student ID must be specified for deletion operations.");
    }
}