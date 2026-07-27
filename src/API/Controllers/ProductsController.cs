using API.Contracts.Products;
using Application.Catalog.Commands.CreateProduct;
using Application.Catalog.Commands.DeleteProduct;
using Application.Catalog.Commands.UpdateProduct;
using Application.Catalog.Queries.GetProductById;
using Application.Catalog.Queries.GetProducts;
using Application.Common.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
      _sender = sender;
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
      var command = new CreateProductCommand(
        request.CategoryId,
        request.Name,
        request.Description,
        request.Sku,
        request.Price,
        request.Currency
      );
      
      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _sender.Send(new GetProductByIdQuery(id));

      if (result.IsFailure)
        return NotFound(result.Error);

      return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
      var result = await _sender.Send(query);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
      var command = new UpdateProductCommand(
        id,
        request.Name,
        request.Description,
        request.Price,
        request.Currency);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var result = await _sender.Send(new DeleteProductCommand(id));

      if (result.IsFailure)
        return NotFound(result.Error);

      return NoContent();
    }
  }
}