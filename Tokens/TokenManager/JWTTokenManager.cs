using Microsoft.IdentityModel.Tokens;
using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Users.Model.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartcouponAPI.Tokens.TokenManager
{
    public class JWTTokenManager : ITokenService
    {
        // Tokens
        private readonly IConfiguration _configuration;

        public JWTTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(ClaimsData data)
        {
            IConfiguration jwtSettings = _configuration.GetSection("Jwt");

            string? stringKey = Environment.GetEnvironmentVariable("JWT_KEY");
            StringBuilder result = new StringBuilder();

            if (stringKey == null)
            {
                result.AppendLine("Clave de firma de JWK no encontrada");

                return result.ToString();
            }

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
                new Claim(JwtRegisteredClaimNames.Iat, data.IssuedAt.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, data.Email),
                new Claim(JwtRegisteredClaimNames.Exp, data.AccessExpirationTime.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            result.Append(tokenString);

            return result.ToString();
        }

        public string GenerateRefreshToken(ClaimsData data)
        {
            IConfiguration jwtSettings = _configuration.GetSection("Jwt");

            string? stringKey = Environment.GetEnvironmentVariable("JWT_KEY");
            StringBuilder result = new StringBuilder();

            if (stringKey == null)
            {
                result.AppendLine("Clave de firma de JWK no encontrada");

                return result.ToString();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(stringKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            StringBuilder fullName = new StringBuilder();

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, data.JWITID.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, data.IssuedAt.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, data.RefreshExpirationTime.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            result.Append(tokenString);

            return result.ToString();
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
