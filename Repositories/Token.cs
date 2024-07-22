using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Repositories;
public class TokenRepository : ITokenRepository
{
    private readonly IConfiguration configuration;

    public TokenRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string CreateJWTToken(IdentityUser identityUser, List<string> roles)
    {
        // create claims
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}