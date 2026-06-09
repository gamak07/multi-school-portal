using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.People.Teachers.Queries.GetTeacherWithSubjects;

public record GetTeacherWithSubjectsQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetTeacherWithSubjectsQueryHandler : IRequestHandler<GetTeacherWithSubjectsQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherWithSubjectsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetTeacherWithSubjectsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
