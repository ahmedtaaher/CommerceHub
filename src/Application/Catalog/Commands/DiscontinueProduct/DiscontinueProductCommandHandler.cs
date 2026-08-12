using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.DiscontinueProduct
{
  public sealed class DiscontinueProductCommandHandler : ICommandHandler<DiscontinueProductCommand>
  {
    private readonly IProductRepository _repository;
    public DiscontinueProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(DiscontinueProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);

      if (product is null)
        return Result.Failure(CatalogErrors.ProductNotFound);

      var result = product.Discontinue();

      if (result.IsFailure)
        return result;

      _repository.Update(product);

      return Result.Success();
    }
  }
}