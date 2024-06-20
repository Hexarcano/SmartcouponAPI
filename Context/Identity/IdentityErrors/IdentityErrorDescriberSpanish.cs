using Microsoft.AspNetCore.Identity;

namespace SmartcouponAPI.Context.Identity.IdentityErrors
{
    public class IdentityErrorDescriberSpanish : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "La contraseña debe tener al menos un carácter no alfanumérico."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "La contraseña debe tener al menos un dígito ('0'-'9')."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "La contraseña debe tener al menos una letra mayúscula ('A'-'Z')."
            };
        }
    }
}
