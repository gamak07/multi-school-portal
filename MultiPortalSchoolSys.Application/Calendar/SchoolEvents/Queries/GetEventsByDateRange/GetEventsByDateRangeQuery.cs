using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Calendar.SchoolEvents.Queries.GetEventsByDateRange;

public record GetEventsByDateRangeQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetEventsByDateRangeQueryHandler : IRequestHandler<GetEventsByDateRangeQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEventsByDateRangeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetEventsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
