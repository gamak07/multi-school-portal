using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Commands.AssignSubjectToTeacher;

public record AssignSubjectToTeacherCommand() : ICommand<Result<int>>;

public sealed class AssignSubjectToTeacherCommandHandler : IRequestHandler<AssignSubjectToTeacherCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public AssignSubjectToTeacherCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(AssignSubjectToTeacherCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
