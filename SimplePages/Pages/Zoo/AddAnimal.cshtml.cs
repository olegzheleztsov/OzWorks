using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimplePages.Models;

namespace SimplePages.Pages.Zoo
{
    public class AddAnimal : PageModel
    {
        [BindProperty] 
        public AnimalSlot AnimalSlot { get; set; } = new AnimalSlot();

        public async Task OnGet()
        {
            
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                return await Task.FromResult(RedirectToPage("Index"));
            }

            return await Task.FromResult(Page());
        }
    }
}