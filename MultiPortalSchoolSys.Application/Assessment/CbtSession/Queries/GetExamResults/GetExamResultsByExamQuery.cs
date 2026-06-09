using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.CbtSession.Queries.GetExamResults;

public record GetExamResultsByExamQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetExamResultsByExamQueryHandler : IRequestHandler<GetExamResultsByExamQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetExamResultsByExamQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetExamResultsByExamQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
