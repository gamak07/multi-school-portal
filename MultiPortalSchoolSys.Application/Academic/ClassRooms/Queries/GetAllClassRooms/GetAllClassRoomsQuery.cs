using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.ClassRooms.Queries.GetAllClassRooms;

public record GetAllClassRoomsQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetAllClassRoomsQueryHandler : IRequestHandler<GetAllClassRoomsQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllClassRoomsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetAllClassRoomsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
