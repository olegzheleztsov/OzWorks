using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SimplePages.Models.GymStats;
using SimplePages.Validators;

namespace SimplePages.ViewModels
{
    public class PhysicalExerciseViewModel
    {
        [Required]
        public string Value { get; set; }
        
        [Required]
        public int ExerciseId { get; set; }
    }
}