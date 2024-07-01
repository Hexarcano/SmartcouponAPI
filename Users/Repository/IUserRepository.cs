using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartcouponAPI.Context.Identity.UserIdentity;
using SmartcouponAPI.Tokens.TokenManager;
using SmartcouponAPI.Users.Model;
using SmartcouponAPI.Users.Model.Requests;
using SmartcouponAPI.Users.Model.Responses;

namespace SmartcouponAPI.Users.Repository
{
    public interface IUserRepository
    {
        Task<UserRegisterResponse> Register(UserRegisterRequest request);
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
