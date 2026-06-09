using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.LeaveRequests.Queries.GetLeaveRequestById;

public record GetLeaveRequestByIdQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeaveRequestByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
