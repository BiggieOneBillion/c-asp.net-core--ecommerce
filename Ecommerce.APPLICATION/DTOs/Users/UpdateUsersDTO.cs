using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Users
{
    /// <summary>
    /// DTO for updating an existing user's information
    /// </summary>
    public record UpdateUsersDTO
    {
        /// <summary>
        /// Updated full name of the user
        /// </summary>
        public required string Name { get; init; } 

        /// <summary>
        /// Updated email address
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Updated password (if applicable)
        /// </summary>
        public string Password { get; init; } = string.Empty;

        /// <summary>
        /// Unique identifier for the user to be updated
        /// </summary>
        public Guid UserId { get; init; }
    }
}