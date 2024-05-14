using JwtBearer.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace JwtBearer.Services;

public class TokenService
{
    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

        //Cria as Credencias do token
        var credencials = new SigningCredentials(
                key:new SymmetricSecurityKey(key), 
                algorithm:SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor= new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            SigningCredentials = credencials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        //Gera o token
        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        
        foreach (var role in user.Roles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        claims.AddClaim(new Claim("Sector","Police"));

        return claims;
    } 
}