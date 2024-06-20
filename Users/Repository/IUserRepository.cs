using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartcouponAPI.Context.Identity.UserIdentity;
using SmartcouponAPI.Users.Model;
using SmartcouponAPI.Users.Model.Requests;
using SmartcouponAPI.Users.Model.Responses;

namespace SmartcouponAPI.Users.Repository
{
    public interface IUserRepository
    {
        Task<UserRegisterResponse> Register(UserRegisterRequest request, UserManager<User> _userManager, UserIdentityDbContext _context);
        Task<UserLoginResponse> Login(UserLoginRequest request, UserManager<User> _userManager, UserIdentityDbContext _context);
    }
}
