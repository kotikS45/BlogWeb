using BlogWebApi.Data;
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
        private readonly AppEFContext _appEFContext;

        public JwtTokenService(IConfiguration configuration, AppEFContext appEFContext)
        {
            _configuration = configuration;
            _appEFContext = appEFContext;
        }

        public async Task<string> CreateTokenAsync(UserEntity user)
        {
            var userRoles = _appEFContext.UserRoles.Where(x => x.UserId == user.Id).ToList();
            var roles = string.Empty;

            foreach (var item in userRoles)
            {
                var role = _appEFContext.Roles.Where(x => x.Id == item.RoleId).First();
                roles += role.Name + ", ";
            }
            roles = roles.TrimEnd(',', ' ');

            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("userName", user.UserName),
                new Claim("roles", roles)
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
