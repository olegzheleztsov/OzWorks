using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimplePages.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AdventureWorksContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, AdventureWorksContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Department> Departments { get; private set; }

        public async Task OnGetAsync(int? number)
        {
            Departments = await _context.Departments.Where(d => d.Name.Length > 2).ToListAsync();
            _logger.LogInformation("Received {Count} departments", Departments.Count());
        }
    }
}