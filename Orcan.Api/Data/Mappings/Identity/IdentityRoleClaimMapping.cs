using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orcan.Api.Data.Mappings.Identity;

public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> b)
    {
        b.ToTable("IdentityRoleClaim");
        b.HasKey(rc => rc.Id);

        b.Property(rc => rc.ClaimType).HasMaxLength(255);
        b.Property(rc => rc.ClaimValue).HasMaxLength(255);
    }
}
