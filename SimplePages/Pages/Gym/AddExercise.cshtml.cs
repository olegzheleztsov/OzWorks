using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SimplePages.Pages.Gym
{
    public class AddExercise : PageModel
    {
        private readonly ILogger<AddExercise> _logger;
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;
        private readonly IExerciseNames _exerciseNames;

        public AddExercise(ILogger<AddExercise> logger, IGymService gymService, IMapper mapper, IExerciseNames exerciseNames)
        {
            _logger = logger;
            _gymService = gymService;
            _mapper = mapper;
            _exerciseNames = exerciseNames;
            Training = new TrainingViewModel() {Date = DateTime.Now, Exercises = new List<PhysicalExerciseViewModel>()};
            FillExerciseNameItems();
        }

        private void FillExerciseNameItems()
        {
            ExerciseNameItems = new List<SelectListItem>();
            var bodyParts = ((BodyPart[]) Enum.GetValues(typeof(BodyPart)));
            foreach (var bodyPart in bodyParts)
            {
                var group = new SelectListGroup() {Name = bodyPart.ToString()};
                var items = _exerciseNames.GetExerciseNames(bodyPart);
                foreach (var nameItem in items)
                {
                    ExerciseNameItems.Add(new SelectListItem()
                    {
                        Value = nameItem.Id.ToString(),
                        Text = nameItem.Name,
                        Group = group
                    });
                }
            }
        }
        
        public TrainingViewModel Training { get; private set; }

        [BindProperty]
        public PhysicalExerciseViewModel Exercise { get; set; } = new PhysicalExerciseViewModel();
        
        [BindProperty]
        public string TrainingId { get; set; }
        
        public List<SelectListItem> ExerciseNameItems { get; private set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogInformation($"Add exercise: to training: {id}");

            TrainingId = id;
            var training = await _gymService.GetAsync(id).ConfigureAwait(false);
            Training = _mapper.Map<Training, TrainingViewModel>(training);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var viewModel = PrepareExerciseViewModel(Exercise);
            var exercise = _mapper.Map<PhysicalExerciseViewModel, PhysicalExercise>(viewModel);
            _logger.LogInformation($"Posted exercise: {JsonConvert.SerializeObject(exercise)} for training: {TrainingId}");

            var training = await _gymService.GetAsync(TrainingId).ConfigureAwait(false);
            training.Exercises ??= new List<PhysicalExercise>();
            training.Exercises.Add(exercise);
            await _gymService.UpdateAsync(TrainingId, training);

            return RedirectToPage("Trainings");
        }

        private PhysicalExerciseViewModel PrepareExerciseViewModel(PhysicalExerciseViewModel physicalExerciseViewModel)
        {
            if (!int.TryParse(physicalExerciseViewModel.Name, out var exerciseNameId))
            {
                throw new ArgumentException($"Invalid exercise name id: {physicalExerciseViewModel.Name}");
            }

            var bodyPart = _exerciseNames.GetBodyPartForExerciseName(exerciseNameId);
            var exerciseName = _exerciseNames.GetExerciseName(exerciseNameId).Name;
            return new PhysicalExerciseViewModel()
            {
                BodyPart = bodyPart,
                Name = exerciseName,
                Value = physicalExerciseViewModel.Value
            };
        }
    }
}