using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Academic.GradingSettings.Queries.GetGradingSettingBySubject;

public record GetGradingSettingBySubjectQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetGradingSettingBySubjectQueryHandler : IRequestHandler<GetGradingSettingBySubjectQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGradingSettingBySubjectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetGradingSettingBySubjectQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
