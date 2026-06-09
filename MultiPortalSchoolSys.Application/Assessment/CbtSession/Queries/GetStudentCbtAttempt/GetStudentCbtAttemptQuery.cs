using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.CbtSession.Queries.GetStudentCbtAttempt;

public record GetStudentCbtAttemptQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetStudentCbtAttemptQueryHandler : IRequestHandler<GetStudentCbtAttemptQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentCbtAttemptQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetStudentCbtAttemptQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
