using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Content.Materials.Queries.GetMaterialsByTeacher;

public record GetMaterialsByTeacherQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetMaterialsByTeacherQueryHandler : IRequestHandler<GetMaterialsByTeacherQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMaterialsByTeacherQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetMaterialsByTeacherQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
