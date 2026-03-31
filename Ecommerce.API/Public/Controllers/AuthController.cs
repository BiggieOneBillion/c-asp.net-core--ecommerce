using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.Register;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.Login;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.Refresh;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.Logout;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.ForgotPassword;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.ResetPassword;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.VerifyEmail;
using Ecommerce.APPLICATION.Features.Auth.Public.Commands.ResendVerification;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Public.Controllers;

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

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register a new user")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand(request));
        return result.ProcessResult(this);
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login and receive tokens")]
    [ProducesResponseType(typeof(GeneralResponse<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new LoginCommand(request, ipAddress));
        
        if (result.IsSuccess)
        {
            SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        }

        return result.ProcessResult(this);
    }

    [HttpPost("refresh")]
    [SwaggerOperation(Summary = "Refresh the access token using a refresh token")]
    [ProducesResponseType(typeof(GeneralResponse<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            var errorResult = Ecommerce.APPLICATION.Common.Models.Result.Failure<Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<AuthResponse>>(
                new Ecommerce.APPLICATION.Common.Models.Error("Error.Unauthorized", "Refresh token is missing."));
            return errorResult.ProcessResult(this);
        }

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var result = await _mediator.Send(new RefreshTokenCommand(new RefreshTokenRequest(refreshToken), ipAddress));
        
        if (result.IsSuccess)
        {
            SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        }

        return result.ProcessResult(this);
    }

    [HttpPost("logout")]
    [SwaggerOperation(Summary = "Logout and invalidate tokens")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
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
                return result.ProcessResult(this);
            }
        }

        Response.Cookies.Delete("refreshToken");
        var successResponse = Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Logged out successfully");
        return Ok(successResponse);
    }

    [HttpPost("forgot-password")]
    [SwaggerOperation(Summary = "Initiate forgot password process")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var result = await _mediator.Send(new ForgotPasswordCommand(request.Email));
        return result.ProcessResult(this);
    }

    [HttpPost("reset-password")]
    [SwaggerOperation(Summary = "Reset password using token")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _mediator.Send(new ResetPasswordCommand(request.Token, request.NewPassword));
        return result.ProcessResult(this);
    }

    [HttpPost("verify-email")]
    [SwaggerOperation(Summary = "Verify user email")]
    [ProducesResponseType(typeof(GeneralResponse<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        var result = await _mediator.Send(new VerifyEmailCommand(request.Token, request.Email));
        
        if (result.IsSuccess)
        {
            SetRefreshTokenCookie(result.Value.Data!.RefreshToken);
        }

        return result.ProcessResult(this);
    }

    [HttpPost("resend-verification")]
    [SwaggerOperation(Summary = "Resend email verification link")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResendVerification([FromBody] ForgotPasswordRequest request)
    {
        var result = await _mediator.Send(new ResendVerificationCommand(request.Email));
        return result.ProcessResult(this);
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
