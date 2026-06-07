using MediatR;
using Microsoft.Extensions.Logging;
using MultiPortalSchoolSys.Application.Common.Interfaces;

namespace MultiPortalSchoolSys.Application.Common.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

    public TransactionBehaviour(IUnitOfWork unitOfWork, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork; 
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)

    {
        if (request is IQuery<TResponse>) { return await next(); }
        try
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed for {Request}", typeof(TRequest).Name);
            throw;
        }
    }
}