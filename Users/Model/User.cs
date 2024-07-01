using Microsoft.AspNetCore.Identity;
using SmartcouponAPI.Tokens.Model;

namespace SmartcouponAPI.Users.Model
{
    public class User : IdentityUser
    {
        public UserData? UserData { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}
