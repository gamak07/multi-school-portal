using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.TheoryQuestions.Commands.DeleteTheoryQuestion;

public record DeleteTheoryQuestionCommand() : ICommand<Result<int>>;

public sealed class DeleteTheoryQuestionCommandHandler : IRequestHandler<DeleteTheoryQuestionCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTheoryQuestionCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(DeleteTheoryQuestionCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
