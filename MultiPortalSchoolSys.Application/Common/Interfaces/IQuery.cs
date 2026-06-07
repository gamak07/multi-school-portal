using MediatR;
namespace MultiPortalSchoolSys.Application.Common.Interfaces;
public interface IQuery<TResponse> : IRequest<TResponse> { }