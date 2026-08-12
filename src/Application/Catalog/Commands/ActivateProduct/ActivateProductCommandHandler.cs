using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.ActivateProduct
{
  public sealed class ActivateProductCommandHandler : ICommandHandler<ActivateProductCommand>
  {
    private readonly IProductRepository _repository;
    public ActivateProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);

      if (product is null)
        return Result.Failure(CatalogErrors.ProductNotFound);

      var result = product.Activate();

      if (result.IsFailure)
        return result;

      _repository.Update(product);

      return Result.Success();
    }
  }
}