using Domain.Shared.Errors;
using MediatR;

namespace Application.Abstractions.Messaging
{
  public interface IQuery<TResponse> : IRequest<Result<TResponse>>
  {
    
  }
}