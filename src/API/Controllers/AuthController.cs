using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Contracts.Auth;
using Application.Auth.ChangePassword;
using Application.Auth.ForgotPassword;
using Application.Auth.GetCurrentUser;
using Application.Auth.Login;
using Application.Auth.Logout;
using Application.Auth.Refresh;
using Application.Auth.Register;
using Application.Auth.ResetPassword;
using Application.Auth.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request)
    {
      var result = await _sender.Send(new LogoutCommand(request.RefreshToken));

      if (result.IsFailure)
        return BadRequest(result.Error);

      return NoContent();
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
      var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

      if (userIdClaim is null)
      {
        return Unauthorized();
      }

      var command = new ChangePasswordCommand(Guid.Parse(userIdClaim), request.CurrentPassword, request.NewPassword);

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
      var result = await _sender.Send(new GetCurrentUserQuery());

      if (result.IsFailure)
        return NotFound(result.Error);

      return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
      var command = new UpdateProfileCommand(request.FirstName, request.LastName);

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return NoContent();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
      var result = await _sender.Send(new ForgotPasswordCommand(request.Email));

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
      var result = await _sender.Send(new ResetPasswordCommand(request.Email, request.Token, request.NewPassword));

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return NoContent();
    }
  }
}