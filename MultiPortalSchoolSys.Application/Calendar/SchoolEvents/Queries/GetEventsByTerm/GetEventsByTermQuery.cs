using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Calendar.SchoolEvents.Queries.GetEventsByTerm;

public record GetEventsByTermQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetEventsByTermQueryHandler : IRequestHandler<GetEventsByTermQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEventsByTermQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetEventsByTermQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
