using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;

namespace SimplePages.Pages.Gym
{
    public class Trainings : PageModel
    {
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;

        public Trainings(IGymService gymService, IMapper mapper)
        {
            _gymService = gymService;
            _mapper = mapper;
        }
        
        public List<TrainingViewModel> TrainingCollection { get; set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var trainings = await _gymService.GetAsync().ConfigureAwait(false);
            TrainingCollection = _mapper.Map<List<Training>, List<TrainingViewModel>>(trainings).OrderByDescending(tr => tr.Date).ToList();
            return Page();
        }
    }
}