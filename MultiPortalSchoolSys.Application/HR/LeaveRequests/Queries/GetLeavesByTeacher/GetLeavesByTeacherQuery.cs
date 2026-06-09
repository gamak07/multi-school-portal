using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.LeaveRequests.Queries.GetLeavesByTeacher;

public record GetLeavesByTeacherQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetLeavesByTeacherQueryHandler : IRequestHandler<GetLeavesByTeacherQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeavesByTeacherQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetLeavesByTeacherQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
