namespace Ecommerce.APPLICATION.DTOs.Auth;

/// <summary>
/// Request model for user registration
/// </summary>
/// <param name="Name">Full name of the user</param>
/// <param name="Email">User's email address (used for login)</param>
/// <param name="Password">User's password</param>
/// <param name="ConfirmPassword">Must match the Password field</param>
public record RegisterRequest(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword);

/// <summary>
/// Request model for user login
/// </summary>
/// <param name="Email">User's email address</param>
/// <param name="Password">User's password</param>
public record LoginRequest(
    string Email,
    string Password);

/// <summary>
/// Response model containing authentication tokens and user details
/// </summary>
/// <param name="AccessToken">JWT access token for authenticating requests</param>
/// <param name="RefreshToken">Token used to obtain a new access token</param>
/// <param name="Name">User's full name</param>
/// <param name="Email">User's email address</param>
/// <param name="Role">Assigned user role (e.g., Admin, Customer)</param>
public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    string Name,
    string Email,
    string Role);

public record RegisterationResponse(
    string Message
)

/// <summary>
/// Request model for refreshing an expired access token
/// </summary>
/// <param name="RefreshToken">The refresh token provided during login or previous refresh</param>
public record RefreshTokenRequest(
    string RefreshToken);

/// <summary>
/// Request model for initiating a password reset
/// </summary>
/// <param name="Email">The email associated with the user account</param>
public record ForgotPasswordRequest(
    string Email);

/// <summary>
/// Request model for resetting a password using a token
/// </summary>
/// <param name="Token">The reset token sent to the user's email</param>
/// <param name="NewPassword">The new password</param>
/// <param name="ConfirmNewPassword">Confirmation of the new password</param>
public record ResetPasswordRequest(
    string Token,
    string NewPassword,
    string ConfirmNewPassword);

/// <summary>
/// Request model for verifying a user's email address
/// </summary>
/// <param name="Token">The verification token sent to the user's email</param>
public record VerifyEmailRequest(
    string Token, string Email);
