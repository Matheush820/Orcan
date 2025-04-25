using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orcan.Api.Data.Mappings.Identity;

public class IdentityUserTokennMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> b)
    {
        b.ToTable("IdentityUserTokens");
        b.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

        b.Property(ut => ut.LoginProvider).HasMaxLength(255);
        b.Property(ut => ut.Name).HasMaxLength(255);
    }
}
