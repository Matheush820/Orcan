using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcan.Core.Models;

namespace Orcan.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(c => c.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Description)
              .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.UserId)
              .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

    }
}
