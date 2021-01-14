using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;

namespace SimplePages.Pages.Gym
{
    public class AddTraining : PageModel
    {
        private readonly ILogger<AddTraining> _logger;
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;

        public AddTraining(ILogger<AddTraining> logger, IGymService gymService, IMapper mapper)
        {
            _logger = logger;
            _gymService = gymService;
            _mapper = mapper;
        }

        [BindProperty]
        public TrainingViewModel Training { get; set; } = new TrainingViewModel()
        {
            Date = DateTime.Now,
            Exercises = new List<PhysicalExerciseViewModel>()
        };
        
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"Posted training: {Newtonsoft.Json.JsonConvert.SerializeObject(Training)}");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Training.Exercises ??= new List<PhysicalExerciseViewModel>();
            var newTraining = _mapper.Map<TrainingViewModel, Training>(Training);
            await _gymService.InsertAsync(newTraining).ConfigureAwait(false);
            return RedirectToPage("Trainings");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await Task.FromResult<IActionResult>(Page());
        }
        
    }
}