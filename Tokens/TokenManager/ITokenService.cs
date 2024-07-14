using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Users.Model.Responses;

namespace SmartcouponAPI.Tokens.TokenManager
{
    public interface ITokenService
    {
        string GenerateAccessToken(ClaimsData data);
        string GenerateRefreshToken(ClaimsData data);
        void RevokeToken(string token);
        bool IsTokenRevoked(string token);
    }
}
