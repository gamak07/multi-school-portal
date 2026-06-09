using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.TheoryQuestions.Commands.AddTheoryQuestion;

public record AddTheoryQuestionCommand() : ICommand<Result<int>>;

public sealed class AddTheoryQuestionCommandHandler : IRequestHandler<AddTheoryQuestionCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public AddTheoryQuestionCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(AddTheoryQuestionCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
