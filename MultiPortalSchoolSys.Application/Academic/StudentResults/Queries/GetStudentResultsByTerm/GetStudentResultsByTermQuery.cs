using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.StudentResults.Queries.GetStudentResultsByTerm;

public record GetStudentResultsByTermQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetStudentResultsByTermQueryHandler : IRequestHandler<GetStudentResultsByTermQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentResultsByTermQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetStudentResultsByTermQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
