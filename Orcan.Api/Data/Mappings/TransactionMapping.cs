using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcan.Core.Models;

namespace Orcan.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(t => t.Type)
            .IsRequired()
         .HasColumnType("SMALLINT");

        builder.Property(t => t.Amount)
         .IsRequired()
         .HasColumnType("MONEY");

        builder.Property(t => t.CreatedAt)
         .IsRequired();

        builder.Property(t => t.PaiOrReceivedAt)
         .IsRequired();

        builder.Property(t => t.UserId)
         .IsRequired()
         .HasColumnType("NVARCHAR")
         .HasMaxLength(160);

    }
}
