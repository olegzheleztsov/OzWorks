// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Production");
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ProductNumber).IsRequired();
            builder.Property(p => p.MakeFlag).IsRequired();
            builder.Property(p => p.FinishedGoodsFlag).IsRequired();
            builder.Property(p => p.Color).IsRequired(false);
            builder.Property(p => p.SafetyStockLevel).IsRequired();
            builder.Property(p => p.ReorderPoint).IsRequired();
            builder.Property(p => p.StandardCost).IsRequired().HasColumnType("money");
            builder.Property(p => p.ListPrice).IsRequired().HasColumnType("money");
            builder.Property(p => p.Size).IsRequired(false);
            builder.Property(p => p.SizeUnitMeasureCode).IsRequired(false);
            builder.Property(p => p.WeightUnitMeasureCode).IsRequired(false);
            builder.Property(p => p.Weight).IsRequired(false).HasColumnType("decimal(8,2)");
            builder.Property(p => p.DaysToManufacture).IsRequired();
            builder.Property(p => p.ProductLine).IsRequired(false);
            builder.Property(p => p.Class).IsRequired(false);
            builder.Property(p => p.Style).IsRequired(false);
            builder.Property(p => p.ProductSubcategoryId).IsRequired(false);
            builder.Property(p => p.ProductModelId).IsRequired(false);
            builder.Property(p => p.SellStartDate).IsRequired();
            builder.Property(p => p.SellEndDate).IsRequired(false);
            builder.Property(p => p.DiscontinuedDate).IsRequired(false);
            builder.Property(p => p.rowguid).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();

            builder.HasOne(p => p.ProductModel);
            builder.HasOne(p => p.ProductSubcategory);
            builder.HasOne(p => p.WeightUnitMeasure).WithOne().HasForeignKey<Product>(p => p.WeightUnitMeasureCode);
            builder.HasOne(p => p.SizeUnitMeasure).WithOne().HasForeignKey<Product>(p => p.SizeUnitMeasureCode);
        }
    }
}