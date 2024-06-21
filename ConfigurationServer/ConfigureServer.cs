using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartcouponAPI.Context.Identity.IdentityErrors;
using SmartcouponAPI.Context.Identity.UserIdentity;
using SmartcouponAPI.Users.Model;
using SmartcouponAPI.Users.Repository;
using System.Text;

namespace SmartcouponAPI.ConfigurationServer
{
    public static class ConfigureServer
    {
        public static void ConfigureConnectionString(IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<UserIdentityDbContext>(
                options => options.UseSqlServer(configurationManager.GetConnectionString("TestDbConnection")));
        }

        public static void ConfigureJwtAuthentication(IServiceCollection services, IConfigurationSection jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        public static void RegisterDependencies(IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<UserIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityErrorDescriberSpanish>();

            services.AddScoped<UserRepository>();
        }
    }
}
