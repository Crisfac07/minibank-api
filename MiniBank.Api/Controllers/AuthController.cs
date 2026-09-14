using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MiniBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        if (username != "john" || password != "password123")
        {
            return Unauthorized();
        }

        var claims = new List<Claim>{
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "Customer")
        };

        var jwtSection = _configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"] ?? 
        throw new InvalidOperationException("JWT SecretKey is not configured");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience : jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            accessToken
        });
    }
    
}