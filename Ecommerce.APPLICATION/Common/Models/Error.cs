namespace Ecommerce.APPLICATION.Common.Models;

/// <summary>
/// Represents an error with a code and message.
/// </summary>
public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
    public static readonly Error NotFound = new("Error.NotFound", "The requested resource was not found");
    public static readonly Error ValidationFailed = new("Error.ValidationFailed", "Validation failed");
    public static readonly Error Conflict = new("Error.Conflict", "A conflict occurred");
    public static readonly Error Unauthorized = new("Error.Unauthorized", "Unauthorized access");
}
