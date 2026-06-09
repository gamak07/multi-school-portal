using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Content.Materials.Commands.DeleteMaterial;

public record DeleteMaterialCommand() : ICommand<Result<int>>;

public sealed class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteMaterialCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
