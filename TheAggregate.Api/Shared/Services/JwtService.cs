using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Shared.Services;

public interface IJwtService
{
    AuthenticationResponse MakeToken(ApplicationUser user);
}

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationResponse MakeToken(ApplicationUser user)
    {
        var expirationMinutes = Convert.ToDouble(_configuration.GetValue<int>("Jwt:ExpirationMinutes"));
        var issuer = _configuration.GetValue<string>("Jwt:Issuer");
        var audience = _configuration.GetValue<string>("Jwt:Audience");
        var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);
        byte[] key;
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
        }
        else
        {
            //TODO: Some key storage API/SDK call here
            key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
        }

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), // Subject (user id)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique id
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at Time
            new Claim(ClaimTypes.NameIdentifier, user.Email!), // Optional: user email
            new Claim(ClaimTypes.NameIdentifier, user.Name) // Optional: user name
        };

        var securityKey = new SymmetricSecurityKey(key);
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenGenerator;
        try
        {
            tokenGenerator = new JwtSecurityToken(issuer, audience, claims, expires: expiration, signingCredentials: creds);
        }
        catch (ArgumentException e)
        {
            return new AuthenticationResponse
            {
                Success = false,
                Errors = [e.Message],
                Message = "Failed to generate JWT token"
            };
        }
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenGenerator);
        return new AuthenticationResponse
        {
            Success = true,
            Message = "Success generating JWT token",
            Token = tokenString,
            Email = user.Email!
        };
    }
}