using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Users.ModelConfiguration
{
    public class UserIdentityModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserName);

            builder.Property(x => x.UserName)
                .IsRequired();
            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasOne(x => x.UserData)
                .WithOne(x => x.UserIdentity)
                .HasForeignKey<UserData>(x => x.UserName)
                .IsRequired();

            builder.Ignore(x => x.Id);
        }
    }
}
