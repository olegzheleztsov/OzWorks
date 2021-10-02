// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePages.Persistence
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory", "Production");
            builder.HasKey(p => p.ProductCategoryId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.rowguid).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
        }
    }
}