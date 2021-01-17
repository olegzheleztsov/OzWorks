using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;

namespace SimplePages.Pages.Gym
{
    public class TrainingDetails : PageModel
    {
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;

        public TrainingDetails(IGymService gymService, IMapper mapper)
        {
            _gymService = gymService;
            _mapper = mapper;
        }
        
        [BindProperty] public TrainingViewModel TrainingViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var training = await _gymService.GetAsync(id).ConfigureAwait(false);
            if (training == null)
            {
                return RedirectToPage("Error");
            }
            
            TrainingViewModel = _mapper.Map<TrainingViewModel>(training);
            return Page();
        }
    }
}