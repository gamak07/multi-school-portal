using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.ClassRooms.Queries.GetClassRoomById;

public record GetClassRoomByIdQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetClassRoomByIdQueryHandler : IRequestHandler<GetClassRoomByIdQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClassRoomByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetClassRoomByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
