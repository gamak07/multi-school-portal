using MediatR;
using MultiPortalSchoolSys.Application.Common.Interfaces;

namespace MultiPortalSchoolSys.Application.Common.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork; // This passes the tool into your private field!
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)

    {
        if (typeof(TRequest).Name.EndsWith("Query")) { return await next(); }
        try
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
        catch (Exception)
        {

            throw;
        }
    }
}