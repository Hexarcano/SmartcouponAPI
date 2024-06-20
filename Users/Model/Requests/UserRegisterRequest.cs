using System.ComponentModel.DataAnnotations;

namespace SmartcouponAPI.Users.Model.Requests
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Ingresa un usuario.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingresa una contraseña.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingresa un apellido paterno.")]
        public string FatherLastName { get; set; }

        [Required(ErrorMessage = "Ingresa un apellido materno.")]
        public string MotherLastName { get; set; }

        [Required(ErrorMessage = "Ingresa un correo electrónico.")]
        [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresa un CURP.")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[0-9]{2}$", ErrorMessage = "Ingresa una CURP válida.")]
        public string CURP { get; set; }
        //public int RoleId { get; set; }
        //public int CompanyId { get; set; }
    }
}
