// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePages.Persistence
{
    public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("ProductModel", "Production");
            builder.HasKey(pm => pm.ProductModelId);
            builder.Property(pm => pm.Name).IsRequired();
            builder.Property(pm => pm.CatalogDescription).IsRequired(false);
            builder.Property(pm => pm.Instructions).IsRequired(false);
            builder.Property(pm => pm.rowguid).IsRequired();
            builder.Property(pm => pm.ModifiedDate).IsRequired();
        }
    }
}