using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Users.Model.Responses;

namespace SmartcouponAPI.Tokens.TokenManager
{
    public interface ITokenService
    {
        string GenerateToken(ClaimsData data);
        void RevokeToken(string token);
        bool IsTokenRevoked(string token);
    }
}
