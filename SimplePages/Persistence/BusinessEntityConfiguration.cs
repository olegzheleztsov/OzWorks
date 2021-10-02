// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePages.Persistence
{
    public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> builder)
        {
            builder.ToTable("BusinessEntity", "Person");
            builder.HasKey(p => p.BusinessEntityId);
            builder.Property(p => p.rowguid).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
        }
    }
}