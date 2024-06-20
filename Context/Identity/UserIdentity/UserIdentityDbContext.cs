using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartcouponAPI.Users.ModelConfiguration;
using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Context.Identity.UserIdentity
{
    public class UserIdentityDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UserData { get; set; }

        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserIdentityModelConfiguration().Configure(builder.Entity<User>());
            new UserDataModelConfiguration().Configure(builder.Entity<UserData>());
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(UserIdentityDbContext).Assembly);
        }
    }
}
