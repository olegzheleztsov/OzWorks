using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;

namespace SimplePages.Validators
{
    public class ExerciseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var exerciseViewModel = validationContext.ObjectInstance as PhysicalExerciseViewModel;
            if (exerciseViewModel == null)
            {
                return new ValidationResult(
                    $"Invalid object instance type: {validationContext.ObjectInstance?.GetType().Name ?? "<Unknown>"}");
            }

            var exerciseName = value as string;
            if (string.IsNullOrEmpty(exerciseName))
            {
                return new ValidationResult("Exercise name should not be null or empty");
            }

            var exerciseNameId = int.Parse(exerciseName);

            var exerciseNames = (IExerciseNames) validationContext.GetService(typeof(IExerciseNames));
            var found = false;
            foreach (var bodyPart in Enum.GetValues<BodyPart>())
            {
                var exerciseNameList = exerciseNames.GetExerciseNames(bodyPart);
                var targetExercise = exerciseNameList.FirstOrDefault(item => item.Id == exerciseNameId);
                if (targetExercise != null)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return new ValidationResult($"Exercise name with id: {exerciseNameId} not found");
            }

            return ValidationResult.Success;
        }
    }
}