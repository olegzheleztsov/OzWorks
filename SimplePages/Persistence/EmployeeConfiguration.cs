// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePages.Persistence
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", "HumanResources");
            builder.HasKey(e => e.BusinessEntityId);
            builder.Property(e => e.NationalIdNumber).IsRequired();
            builder.Property(e => e.LoginId).IsRequired();
            builder.Property(e => e.OrganizationNode);
            builder.Property(e => e.OrganizationLevel).HasComputedColumnSql();
            builder.Property(e => e.JobTitle).IsRequired();
            builder.Property(e => e.BirthDate).IsRequired();
            builder.Property(e => e.MaritalStatus).IsRequired();
            builder.Property(e => e.Gender).IsRequired();
            builder.Property(e => e.HireDate).IsRequired();
            builder.Property(e => e.SalariedFlag).IsRequired();
            builder.Property(e => e.VacationHours).IsRequired();
            builder.Property(e => e.SickLeaveHours).IsRequired();
            builder.Property(e => e.CurrentFlag).IsRequired();
            builder.Property(e => e.rowguid).IsRequired();
            builder.Property(e => e.ModifiedDate).IsRequired();

            builder.HasOne(e => e.Person).WithOne().HasForeignKey<Employee>(e => e.BusinessEntityId);
        }
    }
}