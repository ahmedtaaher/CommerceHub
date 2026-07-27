using API.Contracts.Categories;
using Application.Catalog.Commands.CreateCategory;
using Application.Catalog.Commands.DeleteCategory;
using Application.Catalog.Commands.UpdateCategory;
using Application.Catalog.Queries.GetCategories;
using Application.Catalog.Queries.GetCategoryById;
using Application.Common.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
      _sender = sender;
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _sender.Send(new GetCategoryByIdQuery(id));

      if (result.IsFailure)
        return NotFound(result.Error);

      return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    {
      var result = await _sender.Send(query);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
      var command = new UpdateCategoryCommand(id, request.Name, request.Description);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
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