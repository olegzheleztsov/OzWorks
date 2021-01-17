using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;
using SimplePages.ViewServices;

namespace SimplePages.Pages.Gym
{
    public class EditTraining : PageModel
    {
        private readonly IGymService _gymService;

        private readonly ILogger<EditTraining> _logger;
        private readonly IMapper _mapper;

        public EditTraining(IGymService gymService, IMapper mapper, ILogger<EditTraining> logger,
            IViewHelperService viewHelperService)
        {
            _gymService = gymService;
            _mapper = mapper;
            _logger = logger;
            ExerciseNameItems = viewHelperService.GetExerciseNameListItems();
        }

        [BindProperty] public TrainingViewModel TrainingViewModel { get; set; }

        public List<SelectListItem> ExerciseNameItems { get; }


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

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Received: {Training}", JsonConvert.SerializeObject(TrainingViewModel));
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Trainings");
            }

            var training = _mapper.Map<Training>(TrainingViewModel);
            var success = await _gymService.UpdateAsync(training.Id, training).ConfigureAwait(false);
            if (!success)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("TrainingDetails", new {id = training.Id});
        }
    }
}