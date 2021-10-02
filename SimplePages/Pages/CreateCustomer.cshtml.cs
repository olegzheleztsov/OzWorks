// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimplePages.Persistence;
using SimplePages.Persistence.Contexts;
using System.Threading.Tasks;

namespace SimplePages.Pages
{
    public class CreateCustomer : PageModel
    {
        private readonly CustomDbContext _context;

        public CreateCustomer(CustomDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}