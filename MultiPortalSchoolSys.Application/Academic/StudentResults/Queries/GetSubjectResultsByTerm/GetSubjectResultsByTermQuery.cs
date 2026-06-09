using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.StudentResults.Queries.GetSubjectResultsByTerm;

public record GetSubjectResultsByTermQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetSubjectResultsByTermQueryHandler : IRequestHandler<GetSubjectResultsByTermQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSubjectResultsByTermQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetSubjectResultsByTermQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
