using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SimplePages.Models.GymStats;
using SimplePages.Validators;

namespace SimplePages.ViewModels
{
    public class PhysicalExerciseViewModel
    {
        public BodyPart BodyPart { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        [Required]
        [ExerciseName]
        public string Name { get; set; }
    }
}