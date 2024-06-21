using Microsoft.IdentityModel.Tokens;
using SmartcouponAPI.Users.Model.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartcouponAPI.Token.Model
{
    public class TokenService : ITokenService
    {
        private readonly ConfigurationManager _configurationManager;

        public TokenService(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string GenerateToken(UserLoginResponseData data)
        {
            var jwtSettings = _configurationManager.GetSection("Jwt");
            string stringKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(stringKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            StringBuilder fullName = new StringBuilder();

            fullName.Append(data.Name);
            fullName.Append(" ");
            fullName.Append(data.FatherLastName);
            fullName.Append(" ");
            fullName.Append(data.MotherLastName);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, data.UserName),
                new Claim(JwtRegisteredClaimNames.Name, fullName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, data.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["ExpireDays"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsTokenRevoked(string token)
        {
            throw new NotImplementedException();
        }

        public void RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
