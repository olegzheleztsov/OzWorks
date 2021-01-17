using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;

namespace SimplePages.ViewServices
{
    public class ViewHelperService : IViewHelperService
    {
        private readonly IExerciseNames _exerciseNames;

        public ViewHelperService(IExerciseNames exerciseNames)
        {
            _exerciseNames = exerciseNames;
        }

        public List<SelectListItem> GetExerciseNameListItems()
        {
            var selectListItems = new List<SelectListItem>();
            var bodyParts = Enum.GetValues<BodyPart>();

            foreach (var bodyPart in bodyParts)
            {
                var group = new SelectListGroup
                {
                    Name = bodyPart.ToString()
                };
                var exerciseItems = _exerciseNames.GetExerciseNames(bodyPart);
                selectListItems.AddRange(exerciseItems.Select(
                    exerciseItem => new SelectListItem
                    {
                        Value = exerciseItem.Id.ToString(),
                        Text = exerciseItem.Name,
                        Group = group
                    }));
            }

            return selectListItems;
        }
    }
}