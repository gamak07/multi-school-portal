using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Application.Finance.PaymentReceipts.Queries.GetReceiptsByParent;

public record GetReceiptsByParentQuery() : IQuery<Result<object /* TODO: replace with DTO */>>;

public sealed class GetReceiptsByParentQueryHandler : IRequestHandler<GetReceiptsByParentQuery, Result<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetReceiptsByParentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(GetReceiptsByParentQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query
        throw new NotImplementedException();
    }
}
