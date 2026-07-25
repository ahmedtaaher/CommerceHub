// using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using MediatR;

namespace Application.Behaviors
{
  public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
  {
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

      var response = await next();

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

      return response;
    }
  }
}