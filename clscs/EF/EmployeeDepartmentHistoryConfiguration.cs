// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> builder)
        {
            builder.ToTable("EmployeeDepartmentHistory", "HumanResources");
            builder.HasKey(e => new { e.BusinessEntityId, e.DepartmentId, e.ShiftId, e.StartDate });
            builder.Property(e => e.EndDate).IsRequired(false);
            builder.Property(e => e.ModifiedDate).IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(h => h.DepartmentId);

            builder.HasOne(e => e.Employee).WithMany()
                .HasForeignKey(h => h.BusinessEntityId);

            builder.HasOne(e => e.Shift).WithOne()
                .HasForeignKey<EmployeeDepartmentHistory>(h => h.ShiftId);
        }
    }
}