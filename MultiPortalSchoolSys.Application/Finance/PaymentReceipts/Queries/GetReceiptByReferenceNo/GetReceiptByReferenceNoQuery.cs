using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Finance.PaymentReceipts.Queries.GetReceiptByReferenceNo;

public record GetReceiptByReferenceNoQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetReceiptByReferenceNoQueryHandler : IRequestHandler<GetReceiptByReferenceNoQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetReceiptByReferenceNoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetReceiptByReferenceNoQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
