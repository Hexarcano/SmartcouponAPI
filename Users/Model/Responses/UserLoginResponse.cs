namespace SmartcouponAPI.Users.Model.Responses
{
    public class UserLoginResponse
    {
        public required string Message { get; set; }
        public UserLoginResponseData? Data { get; set; }
    }
}
