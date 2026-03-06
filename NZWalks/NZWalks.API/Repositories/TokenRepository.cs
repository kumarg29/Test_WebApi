using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _config;

        public TokenRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string CreateJwtToken(IdentityUser user, List<string> Roles)
        {
            // Create Claims 
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audiance"],
                Claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: Credentials);


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
