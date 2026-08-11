using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Application.Catalog.Commands.UpdateProduct
{
  public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
  {
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (product is null)
        return Result.Failure(CatalogErrors.ProductNotFound);

      var nameResult = ProductName.Create(request.Name);

      if (nameResult.IsFailure)
        return Result.Failure(nameResult.Error);

      var descriptionResult = ProductDescription.Create(request.Description);

      if (descriptionResult.IsFailure)
        return Result.Failure(descriptionResult.Error);

      var moneyResult = Money.Create(request.Price, request.Currency);

      if (moneyResult.IsFailure)
        return Result.Failure(moneyResult.Error);

      var renameResult = product.Rename(nameResult.Value);

      if (renameResult.IsFailure)
        return renameResult;

      var descriptionChangeResult = product.ChangeDescription(descriptionResult.Value);

      if (descriptionChangeResult.IsFailure)
        return descriptionChangeResult;

      var priceChangeResult = product.ChangePrice(moneyResult.Value);

      if (priceChangeResult.IsFailure)
        return priceChangeResult;

      _repository.Update(product);

      return Result.Success();
    }
  }
}