using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Assessment.CbtQuestions.Queries.GetCbtQuestionsByExam;

public record GetCbtQuestionsByExamQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetCbtQuestionsByExamQueryHandler : IRequestHandler<GetCbtQuestionsByExamQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCbtQuestionsByExamQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetCbtQuestionsByExamQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
