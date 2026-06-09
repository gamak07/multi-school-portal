using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.Sanctions.Queries.GetSanctionsByStudent;

public record GetSanctionsByStudentQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetSanctionsByStudentQueryHandler : IRequestHandler<GetSanctionsByStudentQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSanctionsByStudentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetSanctionsByStudentQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
