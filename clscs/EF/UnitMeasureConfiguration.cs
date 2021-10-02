// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitMeasure> builder)
        {
            builder.ToTable("UnitMeasure", "Production");
            builder.HasKey(p => p.UnitMeasureCode);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
        }
    }
}