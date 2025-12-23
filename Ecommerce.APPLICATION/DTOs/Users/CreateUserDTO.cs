using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Users
{
    public record CreateUserDTO
    {
        public required string Name { get; init; } 

        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

    }
}