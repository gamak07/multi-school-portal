using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.TheoryQuestions.Queries.GetTheoryQuestionsByExam;

public record GetTheoryQuestionsByExamQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetTheoryQuestionsByExamQueryHandler : IRequestHandler<GetTheoryQuestionsByExamQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTheoryQuestionsByExamQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetTheoryQuestionsByExamQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
