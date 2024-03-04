using BlogWebApi.Data.Entities.Identity;
using BlogWebApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogWebApi.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("userName", $"{user.UserName}")
            };
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtKey"));

            var singinKey = new SymmetricSecurityKey(key);

            var singinCredential = new SigningCredentials(singinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: singinCredential,
                expires: DateTime.Now.AddDays(10),
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
