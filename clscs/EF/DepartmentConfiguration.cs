// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department", "HumanResources");
            builder.HasKey(d => d.DepartmentId);
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.GroupName).IsRequired();
            builder.Property(d => d.ModifiedDate).IsRequired();
        }
    }
}