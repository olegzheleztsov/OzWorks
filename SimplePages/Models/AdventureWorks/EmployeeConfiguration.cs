using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplePages.Models.AdventureWorks.HumanResources;

namespace SimplePages.Models.AdventureWorks
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", "HumanResources");
            builder.HasKey(e => e.BusinessEntityId);
            builder.Property(e => e.NationalIdNumber).IsRequired().HasColumnType("nvarchar(15)");
            builder.Property(e => e.LoginId).IsRequired().HasColumnType("nvarchar(256)");
            builder.Property(e => e.OrganizationNode).HasColumnType("hierarchyid");
            builder.Property(e => e.OrganizationLevel).HasComputedColumnSql("[OrganizationNode].[GetLevel]()");
            builder.Property(e => e.JobTitle).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(e => e.BirthDate).IsRequired().HasColumnType("date");
            builder.Property(e => e.MaritalStatus).IsRequired().HasColumnType("nchar(1)");
            builder.Property(e => e.HireDate).IsRequired().HasColumnType("date");
            builder.Property(e => e.SalariedFlag).IsRequired().HasColumnType("[dbo].[Flag]").HasDefaultValueSql("1");
            builder.Property(e => e.VacationHours).IsRequired().HasColumnType("smallint").HasDefaultValueSql("0");
            builder.Property(e => e.SickLeaveHours).IsRequired().HasColumnType("smallint").HasDefaultValueSql("0");
            builder.Property(e => e.CurrentFlag).IsRequired().HasColumnType("[dbo].[Flag]").HasDefaultValueSql("1");
            builder.Property(e => e.RowGuid).IsRequired().HasColumnType("[uniqueidentifier]")
                .HasDefaultValueSql("newid()");
            builder.Property(e => e.ModifiedDate).IsRequired().HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}