using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(p => p.Description)
			.IsRequired()
			.HasColumnType("nvarchar(4000)");

		builder.Property(p => p.Price)
			.HasColumnType("decimal(18,2)");
            
		builder.Property(p => p.PictureUrl)
			.IsRequired();

		builder
			.HasOne(p => p.ProductBrand)
			.WithMany()
			.HasForeignKey(p => p.ProductBrandId);
            
		builder
			.HasOne(p => p.ProductType)
			.WithMany()
			.HasForeignKey(p => p.ProductTypeId);

	}
}