using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.ClassRooms.Queries.GetClassRoomWithDetails;

public record GetClassRoomWithDetailsQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetClassRoomWithDetailsQueryHandler : IRequestHandler<GetClassRoomWithDetailsQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClassRoomWithDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetClassRoomWithDetailsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
