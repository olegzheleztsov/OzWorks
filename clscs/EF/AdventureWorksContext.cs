// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace clscs.EF
{
    public class AdventureWorksContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                Config.ConnectionString,
                conf =>
                {
                    conf.UseHierarchyId();
                });
            DoLogToConsole(optionsBuilder);
        }

        protected void DoLogToConsole(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSubcategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UnitMeasureConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeDepartmentHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new TrackTransactionConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TrackTransaction> Transactions { get; set; }
    }

    public class TrackTransaction
    {
        public Guid Id { get; set; }
    }

    public class TrackTransactionConfiguration : IEntityTypeConfiguration<TrackTransaction>
    {
        public void Configure(EntityTypeBuilder<TrackTransaction> builder)
        {
            builder.ToTable("Transactions", "dbo");
            builder.HasKey(t => t.Id);
        }
    }
}