using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Features.Auth.Commands.Register;
using Ecommerce.APPLICATION.Features.Auth.Commands.Login;
using Ecommerce.APPLICATION.Features.Auth.Commands.Refresh;
using Ecommerce.APPLICATION.Features.Auth.Commands.Logout;
using Ecommerce.APPLICATION.Features.Auth.Commands.ForgotPassword;
using Ecommerce.APPLICATION.Features.Auth.Commands.ResetPassword;
using Ecommerce.APPLICATION.Features.Auth.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.APPLICATION.Features.Auth.Commands.ResendVerification;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand(request));
        if (!result.IsSuccess) return BadRequest(result.Error);

        SetRefreshTokenCookie(result.Value.RefreshToken);
        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new LoginCommand(request, ipAddress));
        if (!result.IsSuccess) return Unauthorized(result.Error);

        SetRefreshTokenCookie(result.Value.RefreshToken);
        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken)) return BadRequest("Refresh token is missing.");

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new RefreshTokenCommand(new RefreshTokenRequest(refreshToken), ipAddress));
        if (!result.IsSuccess) return Unauthorized(result.Error);

        SetRefreshTokenCookie(result.Value.RefreshToken);
        return Ok(result.Value);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _mediator.Send(new LogoutCommand(refreshToken, accessToken));
        }

        Response.Cookies.Delete("refreshToken");
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _mediator.Send(new ForgotPasswordCommand(request.Email));
        return Ok(new { message = "A reset link has been sent to the email." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _mediator.Send(new ResetPasswordCommand(request.Token, request.NewPassword));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(new { message = "Password reset successfully." });
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        var result = await _mediator.Send(new VerifyEmailCommand(request.Token));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(new { message = "Email verified successfully." });
    }

    [HttpPost("resend-verification")]
    public async Task<IActionResult> ResendVerification([FromBody] ForgotPasswordRequest request)
    {
        await _mediator.Send(new ResendVerificationCommand(request.Email));
        return Ok(new { message = "If the email exists and is not verified, a new link has been sent." });
    }

    private void SetRefreshTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Set to true in production
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
}
