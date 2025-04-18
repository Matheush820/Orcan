using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orçan.Api.Data.Mappings.Identity;

public class IdentittUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable("IdentityUserLogins");
        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        builder.Property(ul => ul.LoginProvider).HasMaxLength(255);
        builder.Property(ul => ul.ProviderKey).HasMaxLength(255);
        builder.Property(ul => ul.ProviderDisplayName).HasMaxLength(255);
    }
}
