using Microsoft.IdentityModel.Tokens;
using NZWalk.API.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalk.API.Reposaitories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public  Task<string> CreateTokenAsync(User user)
        {

            //create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.firstname));
            claims.Add(new Claim(ClaimTypes.GivenName, user.lastname));
            claims.Add(new Claim(ClaimTypes.GivenName, user.email));

            //loop into roles of users
            user.roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials );

            return  Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
