using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Tokens.Model
{
    public class Token
    {
        public Guid TokenId { get; set; }
        public string UserName { get; set; }
        public string TokenString { get; set; }
        public string ExpireDate { get; set; }
        public bool IsRevoked { get; set; }
        public User? User { get; set; }
    }
}
