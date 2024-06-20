using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Users.ModelConfiguration
{
    public class UserDataModelConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(x => x.UserName);

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.FatherLastName).IsRequired();
            builder.Property(x => x.MotherLastName).IsRequired();
            builder.Property(x => x.CURP).IsRequired();
        }
    }
}
