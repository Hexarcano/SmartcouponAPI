using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartcouponAPI.Context.Identity.UserIdentity;
using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Tokens.TokenManager;
using SmartcouponAPI.Users.Model;
using SmartcouponAPI.Users.Model.Requests;
using SmartcouponAPI.Users.Model.Responses;
using System.Text;

namespace SmartcouponAPI.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly UserIdentityDbContext _context;
        private readonly JWTTokenManager _tokenManager;

        public UserRepository(UserManager<User> userManager, UserIdentityDbContext context, JWTTokenManager tokenManager)
        {
            _userManager = userManager;
            _context = context;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Valida y registra un nuevo User
        /// </summary>
        /// <param name="request">Datos para registro de un usuario.</param>
        /// <returns>UserRegisterResponse, si se completó exitosamente el registro, UserName es diferente de null</returns>
        public async Task<UserRegisterResponse> Register(UserRegisterRequest request)
        {
            StringBuilder message = new StringBuilder();

            UserRegisterResponse response = new UserRegisterResponse()
            {
                Message = String.Empty,
                UserName = null
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    User newUser = new User()
                    {
                        UserName = request.UserName,
                        PasswordHash = request.Password,
                        Email = request.Email
                    };

                    IdentityResult? userCreationResult = await _userManager.CreateAsync(newUser, newUser.PasswordHash);

                    if (!userCreationResult.Succeeded)
                    {
                        message.AppendLine("Error al crear usuario:");

                        foreach (IdentityError error in userCreationResult.Errors)
                        {
                            message.AppendLine(error.Description);
                        }

                        response.Message = message.ToString();
                        await transaction.RollbackAsync();
                        return response;
                    }

                    UserData newUserData = new UserData()
                    {
                        UserName = newUser.UserName,
                        Name = request.Name,
                        FatherLastName = request.FatherLastName,
                        MotherLastName = request.MotherLastName,
                        CURP = request.CURP
                    };

                    await _context.UsersData.AddAsync(newUserData);
                    int affectedRows = _context.SaveChanges();

                    if (affectedRows < 1)
                    {
                        message.AppendLine("Error al conectar con la base de datos. Inténtelo más tarde.");

                        response.Message = message.ToString();
                        await transaction.RollbackAsync();

                        return response;
                    }

                    await transaction.CommitAsync();

                    message.AppendLine("Usuario creado con éxito");

                    response.Message = message.ToString();
                    response.UserName = newUser.UserName;

                    return response;
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    response.Message = "Ocurrió un error al guardar los datos. Por favor, inténtelo más tarde.";

                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.Message = "Ocurrió con la base de datos. Por favor, inténtelo más tarde.";

                    return response;
                }
            }
        }

        /// <summary>
        /// Realiza la validación y regresa un "mensaje" y "los datos basicos" para una sesión.
        /// </summary>
        /// <param name="request">Datos para inicio de sesión.</param>
        /// <returns>UserLoginResponse, si se completó la validación, Data es diferente de null</returns>
        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            StringBuilder message = new StringBuilder();

            UserLoginResponse response = new UserLoginResponse()
            {
                Message = String.Empty,
                Data = null
            };

            User? user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                message.AppendLine("El usuario no existe.");
                response.Message = message.ToString();
                response.ResponseType = Requests.EResponseType.BadRequest;

                return response;
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                message.AppendLine("Contraseña incorrecta.");
                response.Message = message.ToString();
                response.ResponseType = Requests.EResponseType.BadRequest;

                return response;
            }

            try
            {
                UserData? userData = _context.UsersData.FirstOrDefault(x => x.UserName == user.UserName);

                if (userData == null)
                {
                    message.AppendLine("Error al obtener los datos del usuario.");
                    response.Message = message.ToString();
                    response.ResponseType = Requests.EResponseType.BadRequest;

                    return response;
                }

                DateTime issuedAt = DateTime.Now;
                DateTime accessExpirationTime = issuedAt.AddDays(1);
                DateTime refreshExpirationTime = issuedAt.AddDays(7);

                ClaimsData claimsData = new ClaimsData()
                {
                    JWITID = Guid.NewGuid(),
                    UserName = user.UserName,
                    Name = userData.Name,
                    FatherLastName = userData.FatherLastName,
                    MotherLastName = userData.MotherLastName,
                    Email = user.Email,
                    IssuedAt = issuedAt,
                    AccessExpirationTime = accessExpirationTime,
                    RefreshExpirationTime = refreshExpirationTime,
                };

                string accessToken = _tokenManager.GenerateAccessToken(claimsData);
                string refreshToken = _tokenManager.GenerateRefreshToken(claimsData);

                RefreshToken token = new RefreshToken()
                {
                    TokenId = claimsData.JWITID,
                    UserName = user.UserName,
                    Token = refreshToken,
                    ExpireDate = refreshExpirationTime.ToString(),
                    IsRevoked = false
                };

                await _context.Tokens.AddAsync(token);
                int affectedRow = _context.SaveChanges();

                UserLoginResponseData data = new UserLoginResponseData()
                {
                    UserName = userData.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };

                message.Append("Bienvenido: ");
                message.Append(userData.Name);

                response.Message = message.ToString();
                response.Data = data;

                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Message = "Ocurrió un error al guardar los datos. Por favor, inténtelo más tarde.";

                return response;
            }
            catch (DbUpdateException ex)
            {
                response.Message = "Ocurrió con la base de datos. Por favor, inténtelo más tarde.";

                return response;
            }
        }
    }
}
