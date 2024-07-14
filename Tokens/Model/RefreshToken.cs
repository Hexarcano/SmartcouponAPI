using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Tokens.Model
{
    public class RefreshToken
    {
        public Guid TokenId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string ExpireDate { get; set; }
        public bool IsRevoked { get; set; }
        public User? User { get; set; }
    }
}
