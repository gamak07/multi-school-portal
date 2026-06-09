using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.CbtSession.Commands.SubmitCbtAttempt;

public record SubmitCbtAttemptCommand() : ICommand<Result<int>>;

public sealed class SubmitCbtAttemptCommandHandler : IRequestHandler<SubmitCbtAttemptCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public SubmitCbtAttemptCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(SubmitCbtAttemptCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
