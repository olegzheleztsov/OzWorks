// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePages.Persistence
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shift", "HumanResources");
            builder.HasKey(s => s.ShiftId);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();
            builder.Property(s => s.ModifiedDate).IsRequired();
        }
    }
}