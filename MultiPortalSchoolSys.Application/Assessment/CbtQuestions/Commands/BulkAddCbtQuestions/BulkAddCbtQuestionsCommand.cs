using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.CbtQuestions.Commands.BulkAddCbtQuestions;

public record BulkAddCbtQuestionsCommand() : ICommand<Result<int>>;

public sealed class BulkAddCbtQuestionsCommandHandler : IRequestHandler<BulkAddCbtQuestionsCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public BulkAddCbtQuestionsCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(BulkAddCbtQuestionsCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
