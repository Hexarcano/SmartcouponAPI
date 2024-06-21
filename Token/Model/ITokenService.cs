using SmartcouponAPI.Users.Model.Responses;

namespace SmartcouponAPI.Token.Model
{
    public interface ITokenService
    {
        string GenerateToken(UserLoginResponseData data);
        void RevokeToken(string token);
        bool IsTokenRevoked(string token);
    }
}
