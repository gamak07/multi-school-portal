using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Commands.RemoveSubjectFromTeacher;

public record RemoveSubjectFromTeacherCommand() : ICommand<Result<int>>;

public sealed class RemoveSubjectFromTeacherCommandHandler : IRequestHandler<RemoveSubjectFromTeacherCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public RemoveSubjectFromTeacherCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(RemoveSubjectFromTeacherCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
