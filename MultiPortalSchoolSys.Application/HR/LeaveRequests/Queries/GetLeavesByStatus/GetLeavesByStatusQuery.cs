using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.LeaveRequests.Queries.GetLeavesByStatus;

public record GetLeavesByStatusQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetLeavesByStatusQueryHandler : IRequestHandler<GetLeavesByStatusQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeavesByStatusQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetLeavesByStatusQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
