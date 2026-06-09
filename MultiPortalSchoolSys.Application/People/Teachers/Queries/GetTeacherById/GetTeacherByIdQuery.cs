using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Queries.GetTeacherById;

public record GetTeacherByIdQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
