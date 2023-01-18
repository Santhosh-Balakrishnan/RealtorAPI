using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealtorAPI.Utilities
{
    public class JWTTokenGenerator
    {
        public static string GenerateJWTToken(User user, string apiKey)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(typeof(Roles).FullName, user.Role));
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey));
            var credential=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:credential);
            var token=new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
