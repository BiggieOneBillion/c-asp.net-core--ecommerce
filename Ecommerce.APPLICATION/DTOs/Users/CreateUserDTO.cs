using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Users
{
    /// <summary>
    /// DTO for creating a new user record
    /// </summary>
    public record CreateUserDTO
    {
        /// <summary>
        /// Full name of the user
        /// </summary>
        public required string Name { get; init; } 

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// User's chosen password
        /// </summary>
        public string Password { get; init; } = string.Empty;
    }
}