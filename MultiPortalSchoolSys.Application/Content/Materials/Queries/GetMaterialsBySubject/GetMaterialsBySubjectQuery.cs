using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Content.Materials.Queries.GetMaterialsBySubject;

public record GetMaterialsBySubjectQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetMaterialsBySubjectQueryHandler : IRequestHandler<GetMaterialsBySubjectQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMaterialsBySubjectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetMaterialsBySubjectQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
