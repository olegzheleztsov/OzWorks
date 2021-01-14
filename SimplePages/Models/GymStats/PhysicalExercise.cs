using System.Collections.Generic;

namespace SimplePages.Models.GymStats
{
    public class PhysicalExercise
    {
        public BodyPart BodyPart { get; set; }
        
        public string Name { get; set; }
        
        public string Value { get; set; }
    }
}