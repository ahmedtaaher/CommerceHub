using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Application.Catalog.Commands.CreateProduct
{
  public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
  {
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

      var nameResult = ProductName.Create(request.Name);

      if (nameResult.IsFailure)
        return Result<Guid>.Failure(nameResult.Error);

      var descriptionResult = ProductDescription.Create(request.Description);

      if (descriptionResult.IsFailure)
        return Result<Guid>.Failure(descriptionResult.Error);

      var skuResult = Sku.Create(request.Sku);

      if (skuResult.IsFailure)
        return Result<Guid>.Failure(skuResult.Error);

      var skuExists = await _productRepository.ExistsBySkuAsync(skuResult.Value, cancellationToken);

      if (skuExists)
        return Result<Guid>.Failure(CatalogErrors.SkuAlreadyExists);

      var priceResult = Money.Create(request.Price, request.Currency);

      if (priceResult.IsFailure)
        return Result<Guid>.Failure(priceResult.Error);

      var productId = Guid.NewGuid();

      var productResult = Product.Create(
        productId,
        nameResult.Value,
        descriptionResult.Value,
        skuResult.Value,
        priceResult.Value);

      if (productResult.IsFailure)
        return Result<Guid>.Failure(productResult.Error);

      await _productRepository.AddAsync(productResult.Value, cancellationToken);

      return Result<Guid>.Success(productResult.Value.Id);
    }
  }
}