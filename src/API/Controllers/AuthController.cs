using API.Contracts.Auth;
using Application.Auth.Login;
using Application.Auth.Refresh;
using Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
      _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
      var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
      var command = new LoginCommand(request.Email, request.Password);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return Unauthorized(result.Error);

      return Ok(result.Value);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
      var command = new RefreshTokenCommand(
        request.RefreshToken);

      var result = await _sender.Send(command);

      if (result.IsFailure)
        return Unauthorized(result.Error);

      return Ok(result.Value);
    }
  }
}