using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimplePages.Persistence;

namespace SimplePages.Pages;

public class DepartmentsModel : PageModel
{
    public DepartmentsModel(AdventureWorksContext context, ILogger<DepartmentsModel> logger)
    {
        Context = context;
        Logger = logger;
    }

    public AdventureWorksContext Context { get; }
    public ILogger<DepartmentsModel> Logger { get; }

    public IEnumerable<Department> Departments { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Departments = await Context.Departments.OrderBy(d => d.Name).ToListAsync().ConfigureAwait(false);
        return Page();
    }
}
