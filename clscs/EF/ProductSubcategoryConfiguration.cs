// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class ProductSubcategoryConfiguration : IEntityTypeConfiguration<ProductSubcategory>
    {
        public void Configure(EntityTypeBuilder<ProductSubcategory> builder)
        {
            builder.ToTable("ProductSubcategory", "Production");
            builder.HasKey(p => p.ProductSubcategoryId);
            builder.Property(p => p.ProductCategoryId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.rowguid).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
            builder.HasOne(p => p.ProductCategory).WithMany();
        }
    }
}