using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand() : ICommand<Result<int>>;

public sealed class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateTeacherCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
