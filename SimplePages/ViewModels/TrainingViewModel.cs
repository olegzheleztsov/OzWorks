using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SimplePages.ViewModels
{
    public class TrainingViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        public List<PhysicalExerciseViewModel> Exercises { get; set; }
    }
}