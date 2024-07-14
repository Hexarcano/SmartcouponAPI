using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartcouponAPI.Tokens.Model;
using SmartcouponAPI.Users.Model;

namespace SmartcouponAPI.Tokens.ModelConfiguration
{
    public class TokenModelConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.TokenId);

            builder.Property(x => x.TokenId)
                .IsRequired();
            builder.Property(x => x.UserName)
                .IsRequired();
            builder.Property(x => x.Token)
                .IsRequired();
            builder.Property(x => x.ExpireDate)
                .IsRequired();
            builder.Property(x => x.IsRevoked)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserName)
                .IsRequired();
        }
    }
}
