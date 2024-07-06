using Microsoft.IdentityModel.Tokens;
using PRN231.Domain.Constants;
using PRN231.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN231.Application.Helpers;

public static class JwtHelpers
{
    private static readonly string _key = EnvironmentHelpers.GetJwtKey();
    public static string GenerateToken(JwtModel request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(_key);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, request.Id.ToString()),
            new(ClaimTypes.Email, request.Email),
            new(ClaimTypes.Name, request.Name),
            new(ClaimTypes.Role, request.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(TokenConstants.ExpireTimeInMin), // Expires time
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), 
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
