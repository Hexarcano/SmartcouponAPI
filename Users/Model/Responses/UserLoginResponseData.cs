namespace SmartcouponAPI.Users.Model.Responses
{
    public class UserLoginResponseData
    {
        public required string UserName { get; set; }
        //public int RoleId { get; set; }
        //public int CompanyId { get; set; }
        public string? Token { get; set; }
    }
}
