using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orçan.Api.Data.Mappings.Identity;

public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<long>> b)
    {
        b.ToTable("IdentityRole");
        b.HasKey(r => r.Id);

        b.HasIndex(r => r.NormalizedName).IsUnique();
        b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
        b.Property(r => r.Name).HasMaxLength(256);
        b.Property(r => r.NormalizedName).HasMaxLength(256);
    }
}
