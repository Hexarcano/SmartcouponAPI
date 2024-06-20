using System.ComponentModel.DataAnnotations;

namespace SmartcouponAPI.Users.Model.Requests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Ingresa un usuario.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingresa una contraseña.")]
        public string Password { get; set; }
    }
}
