namespace Ecommerce.APPLICATION.DTOs.Auth;

public record RegisterRequest(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword);

public record LoginRequest(
    string Email,
    string Password);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    string Name,
    string Email,
    string Role);

public record RefreshTokenRequest(
    string RefreshToken);

public record ForgotPasswordRequest(
    string Email);

public record ResetPasswordRequest(
    string Token,
    string NewPassword,
    string ConfirmNewPassword);

public record VerifyEmailRequest(
    string Token);
