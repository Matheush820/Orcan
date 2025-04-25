using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orcan.Api.Data.Mappings.Identity;

public class IdentityUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> b)
    {
        b.ToTable("IdentityClaim");
        b.HasKey(uc => uc.Id);
        b.Property(u => u.ClaimType).HasMaxLength(255);
        b.Property(u => u.ClaimValue).HasMaxLength(255);
    }
}
