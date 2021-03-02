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
    public class AddExercise : PageModel
    {
        private readonly IGymService _gymService;
        private readonly ILogger<AddExercise> _logger;
        private readonly IMapper _mapper;

        public AddExercise(ILogger<AddExercise> logger, IGymService gymService,
            IMapper mapper, IViewHelperService viewHelperService)
        {
            _logger = logger;
            _gymService = gymService;
            _mapper = mapper;
            ExerciseNameItems = viewHelperService.GetExerciseNameListItems();
        }

        [BindProperty] public PhysicalExerciseViewModel Exercise { get; set; } = new PhysicalExerciseViewModel();

        [BindProperty] public string TrainingId { get; set; }

        public List<SelectListItem> ExerciseNameItems { get; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogInformation("Add exercise: to training: {TrainingId}", id);
            TrainingId = id;
            return await Task.FromResult(Page()).ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var exercise = _mapper.Map<PhysicalExerciseViewModel, PhysicalExercise>(Exercise);
            _logger.LogInformation("Posted exercise: {Exercise} for training: {TrainingId}",
                JsonConvert.SerializeObject(exercise), TrainingId);

            var training = await _gymService.GetAsync(TrainingId).ConfigureAwait(false);
            training.Exercises ??= new List<PhysicalExercise>();
            training.Exercises.Add(exercise);
            await _gymService.UpdateAsync(TrainingId, training);

            return RedirectToPage("TrainingDetails", new
            {
                id = TrainingId
            });
        }
    }
}