using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;
    private readonly IPermissionProvider _permissionProvider;

    public JwtTokenGenerator(IConfiguration configuration, IPermissionProvider permissionProvider)
    {
        _configuration = configuration;
        _permissionProvider = permissionProvider;
    }

    public string GenerateAccessToken(Users user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? "your-very-strong-secret-key-that-is-at-least-256-bits");
        var tokenHandler = new JwtSecurityTokenHandler();

        var permissions = _permissionProvider.GetPermissionsForRole(user.Role);
        
        var claims = new List<Claim>
        {
            new("id", user.Id.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add permissions as custom claims
        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
