using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.LeaveRequests.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand() : ICommand<Result<int>>;

public sealed class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
