using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Commom.Services
{
    public static class JWTServices
    {
        public static string Generate(string name, string discriminator, Guid idUser, int minutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OKEntrega-b71e507ae8f44b4396530166279942af"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("name", name),
                new Claim("discriminator", discriminator),
                new Claim(JwtRegisteredClaimNames.Jti, idUser.ToString()),
            };

            var token = new JwtSecurityToken
                (
                    "OKEntrega",
                    "OKEntrega",
                    claims,
                    expires: DateTime.Now.AddMinutes(minutes),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
