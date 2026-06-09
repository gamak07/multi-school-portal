using FluentValidation;
using MultiPortalSchoolSys.Application.Common.Interfaces;

namespace MultiPortalSchoolSys.Application.People.Students.Commands.CreateStudent;

// 3. THE VALIDATOR (The Automated Security Checkpoint)
public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    // Injecting IDateTime here guarantees our validation layer remains pure and testable
    public CreateStudentCommandValidator(IDateTime dateTime)
    {
        RuleFor(v => v.UserId)
            .GreaterThan(0)
            .WithMessage("The Student's linked User Account ID must be valid.");

        RuleFor(v => v.ParentId)
            .GreaterThan(0)
            .WithMessage("A student must be explicitly linked to a valid Parent ID.");

        // Safe evaluation path for the optional ClassRoomId configuration parameter
        RuleFor(v => v.ClassRoomId)
            .GreaterThan(0)
            .When(v => v.ClassRoomId.HasValue)
            .WithMessage("If specified, the Classroom ID must be a valid positive integer.");

        RuleFor(v => v.AdmissionNo)
            .NotEmpty()
            .WithMessage("Admission Number is completely mandatory.")
            .MaximumLength(50)
            .WithMessage("Admission Number cannot exceed 50 characters.");

        RuleFor(v => v.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of Birth must be specified.")
            .LessThan(v => dateTime.UtcNow)
            .WithMessage("Date of Birth cannot be a date in the future.");

        RuleFor(v => v.EnrollmentDate)
            .NotEmpty()
            .WithMessage("Enrollment Date must be specified.")
            .LessThanOrEqualTo(v => dateTime.UtcNow)
            .WithMessage("Enrollment Date cannot be a date in the future.");
    }
}