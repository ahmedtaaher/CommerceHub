using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.DeleteProduct
{
  public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
  {
    private readonly IProductRepository _repository;
    public DeleteProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (product is null)
        return Result.Failure(CatalogErrors.ProductNotFound);

      _repository.Remove(product);

      return Result.Success();
    }
  }
}