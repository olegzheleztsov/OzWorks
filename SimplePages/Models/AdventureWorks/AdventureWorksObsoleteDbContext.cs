using Microsoft.EntityFrameworkCore;
using SimplePages.Models.AdventureWorks.HumanResources;

namespace SimplePages.Models.AdventureWorks
{
    public class AdventureWorksObsoleteDbContext : DbContext
    {
        public AdventureWorksObsoleteDbContext(DbContextOptions<AdventureWorksObsoleteDbContext> options)
            : base(options) {}
        
        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}