using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.DeactivateProduct
{
  public sealed class DeactivateProductCommandHandler : ICommandHandler<DeactivateProductCommand>
  {
    private readonly IProductRepository _repository;
    public DeactivateProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);

      if (product is null)
        return Result.Failure(CatalogErrors.ProductNotFound);

      var result = product.Deactivate();

      if (result.IsFailure)
        return result;

      _repository.Update(product);

      return Result.Success();
    }
  }
}