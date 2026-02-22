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
using Ecommerce.APPLICATION.ResponseDTOs;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for authentication and authorization operations
/// </summary>
[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request">Registration details</param>
    /// <returns>Status message</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand(request));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));

        return StatusCode(result.Value.StatusCode, result.Value);
    }

    /// <summary>
    /// Login and receive tokens
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>User details and tokens</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new LoginCommand(request, ipAddress));
        if (!result.IsSuccess) return Unauthorized(GeneralResponse<object>.CreateFailure(result.Error.Message, 401));

        SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Refresh the access token using a refresh token
    /// </summary>
    /// <returns>New user details and tokens</returns>
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken)) return BadRequest(GeneralResponse<object>.CreateFailure("Refresh token is missing.", 400));

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new RefreshTokenCommand(new RefreshTokenRequest(refreshToken), ipAddress));
        if (!result.IsSuccess) return Unauthorized(GeneralResponse<object>.CreateFailure(result.Error.Message, 401));

        SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Logout and invalidate tokens
    /// </summary>
    /// <returns>Success message</returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(refreshToken))
        {
            var result = await _mediator.Send(new LogoutCommand(refreshToken, accessToken));
            if (result.IsSuccess)
            {
                Response.Cookies.Delete("refreshToken");
                return Ok(result.Value);
            }
        }

        Response.Cookies.Delete("refreshToken");
        return Ok(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Logged out successfully"));
    }

    /// <summary>
    /// Initiate forgot password process
    /// </summary>
    /// <param name="request">Email address</param>
    /// <returns>Success message</returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _mediator.Send(new ForgotPasswordCommand(request.Email));
        return Ok(new { message = "A reset link has been sent to the email." });
    }

    /// <summary>
    /// Reset password using token
    /// </summary>
    /// <param name="request">Token and new password</param>
    /// <returns>Success message</returns>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _mediator.Send(new ResetPasswordCommand(request.Token, request.NewPassword));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(new { message = "Password reset successfully." });
    }

    /// <summary>
    /// Verify user email
    /// </summary>
    /// <param name="request">Verification token</param>
    /// <returns>Auth details and tokens</returns>
    [HttpPost("verify-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        var result = await _mediator.Send(new VerifyEmailCommand(request.Token, request.Email));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        
        SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Resend email verification link
    /// </summary>
    /// <param name="request">Email address</param>
    /// <returns>Success message</returns>
    [HttpPost("resend-verification")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
