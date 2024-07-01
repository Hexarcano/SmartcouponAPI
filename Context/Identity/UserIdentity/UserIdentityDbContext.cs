using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartcouponAPI.Users.ModelConfiguration;
using SmartcouponAPI.Users.Model;
using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Tokens.ModelConfiguration;

namespace SmartcouponAPI.Context.Identity.UserIdentity
{
    public class UserIdentityDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new UserIdentityModelConfiguration().Configure(builder.Entity<User>());
            new UserDataModelConfiguration().Configure(builder.Entity<UserData>());
            new TokenModelConfiguration().Configure(builder.Entity<Token>());

            builder.ApplyConfigurationsFromAssembly(typeof(UserIdentityDbContext).Assembly);
        }
    }
}
