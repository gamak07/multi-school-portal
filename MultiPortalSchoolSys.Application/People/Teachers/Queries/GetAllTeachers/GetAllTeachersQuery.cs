using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Queries.GetAllTeachers;

public record GetAllTeachersQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTeachersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
