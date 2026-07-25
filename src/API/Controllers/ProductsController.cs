using Application.Catalog.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
      _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
      return Ok();
    }
  }
}