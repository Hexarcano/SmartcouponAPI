using Microsoft.AspNetCore.Identity;
using SmartcouponAPI.Tokens.Model;

namespace SmartcouponAPI.Users.Model
{
    public class User : IdentityUser
    {
        public UserData? UserData { get; set; }
        public ICollection<RefreshToken> Tokens { get; set; }
    }
}
