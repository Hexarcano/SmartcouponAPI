using SmartcouponAPI.Requests;
using System.Text.Json.Serialization;

namespace SmartcouponAPI.Users.Model.Responses
{
    public class UserLoginResponse
    {
        public required string Message { get; set; }
        public UserLoginResponseData? Data { get; set; }

        [JsonIgnore]
        public EResponseType? ResponseType { get; set; }
    }
}
