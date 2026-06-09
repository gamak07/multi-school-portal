using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.HR.Sanctions.Queries.GetSanctionsByTeacher;

public record GetSanctionsByTeacherQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetSanctionsByTeacherQueryHandler : IRequestHandler<GetSanctionsByTeacherQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSanctionsByTeacherQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetSanctionsByTeacherQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
