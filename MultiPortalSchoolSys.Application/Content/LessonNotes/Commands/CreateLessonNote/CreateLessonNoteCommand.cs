using MediatR;
using MultiPortalSchoolSys.Application.Common.Models;
using MultiPortalSchoolSys.Application.Interfaces;
using MultiPortalSchoolSys.Domain.Entities.Content; 

namespace MultiPortalSchoolSys.Application.Content.LessonNotes.Commands.CreateLessonNote;

// 1. THE COMMAND (The Envelope)
public record CreateLessonNoteCommand(
    string Title,
    string DocumentUrl,
    int SubjectId,
    int AcademicTermId,
    int WeekNumber) : IRequest<Result<int>>;

// 2. THE HANDLER (The Engine Room)
public class CreateLessonNoteCommandHandler : IRequestHandler<CreateLessonNoteCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    // Inject our database unit of work coordinator
    public CreateLessonNoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateLessonNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Step A: Create the new domain model instance using your constructor rules
            var lessonNote = new LessonNote(
                request.Title,
                request.DocumentUrl,
                request.SubjectId,
                request.AcademicTermId,
                request.WeekNumber
            );

            // Step B: Stage the object into the Unit of Work tracking context
            // Note: If your IUnitOfWork uses a generic repository method like .Repository<T>(), use that here!
            // Example: await _unitOfWork.Repository<LessonNote>().AddAsync(lessonNote);
            await _unitOfWork.LessonNotes.AddAsync(lessonNote);

            // Step C: Return a successful Result payload containing the entity ID
            // (Your global TransactionBehaviour will automatically catch this and call SaveChangesAsync for you!)
            return Result<int>.Success(lessonNote.Id);
        }
        catch (ArgumentException ex)
        {
            // Catch any parameter guard validation failures thrown by your Domain rules
            return Result<int>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            // Catch unexpected runtime problems safely
            return Result<int>.Failure($"An error occurred while creating the lesson note: {ex.Message}");
        }
    }
}