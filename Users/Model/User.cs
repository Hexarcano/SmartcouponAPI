using Microsoft.AspNetCore.Identity;

namespace SmartcouponAPI.Users.Model
{
    public class User : IdentityUser
    {
        public UserData UserData { get; set; }
    }
}
