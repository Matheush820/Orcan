using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orçan.Core.Models;

namespace Orçan.Api.Data.Mappings
{
    public class CategoryMappings : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Mapeamento para a tabela 'Category' no banco de dados
            builder.ToTable("Category");

            // Outras configurações, como chave primária e propriedades
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                    .HasColumnType("NVARCHAR")
                   .HasMaxLength(255);

            builder.Property(c => c.UserId)
                   .IsRequired()
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(160);
        }
    }
}
