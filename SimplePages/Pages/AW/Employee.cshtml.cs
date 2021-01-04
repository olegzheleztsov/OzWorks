using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplePages.Models.AdventureWorks;

namespace SimplePages.Pages.AW
{
    public class Employee : PageModel
    {
        private readonly AdventureWorksDbContext _context;

        public Employee(AdventureWorksDbContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            
        }
        
        public string EmployeesSummary { get; private set; }
        public  string FirstEmployee { get; private set; }

        public async Task<IActionResult> OnGetEmployeesAsync()
        {
            int employeesCount = await _context.Employees.CountAsync().ConfigureAwait(false);
            var firstEmployee = await _context.Employees.FirstAsync().ConfigureAwait(false);
            EmployeesSummary = $"Count of employees: {employeesCount}";
            FirstEmployee = JsonConvert.SerializeObject(firstEmployee);
            return Page();
        }
    }
}