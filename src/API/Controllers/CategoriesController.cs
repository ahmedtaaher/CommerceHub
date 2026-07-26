using API.Contracts.Categories;
using Application.Catalog.Commands.CreateCategory;
using Application.Catalog.Commands.DeleteCategory;
using Application.Catalog.Commands.UpdateCategory;
using Application.Catalog.Queries.GetCategories;
using Application.Catalog.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
      _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _sender.Send(new GetCategoryByIdQuery(id));

      if (result.IsFailure)
        return NotFound(result.Error);

      return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    {
      var result = await _sender.Send(query);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
      var command = new UpdateCategoryCommand(id, request.Name, request.Description);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var result = await _sender.Send(new DeleteCategoryCommand(id));

      if (result.IsFailure)
        return NotFound(result.Error);

      return NoContent();
    }
  }
}