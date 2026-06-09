using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.Sanctions.Queries.GetSanctionsByResolutionStatus;

public record GetSanctionsByResolutionStatusQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetSanctionsByResolutionStatusQueryHandler : IRequestHandler<GetSanctionsByResolutionStatusQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSanctionsByResolutionStatusQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetSanctionsByResolutionStatusQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
