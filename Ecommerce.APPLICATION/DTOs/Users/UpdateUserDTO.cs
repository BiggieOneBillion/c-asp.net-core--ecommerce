namespace Ecommerce.APPLICATION.DTOs.Users
{
    /// <summary>
    /// DTO for updating an existing user's information
    /// </summary>
    public record UpdateUserDTO
    {
        /// <summary>
        /// Updated full name of the user
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Updated email address
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Updated password (optional)
        /// </summary>
        public string? Password { get; init; } = null;
    }
}