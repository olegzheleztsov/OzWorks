using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimplePages.Models
{
    public class Animal
    {
        [DisplayName("Animal name")]
        [Required]
        public string Name { get; set; }
        public AnimalSex Sex { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        
        [Required]
        public AnimalKind Kind { get; set; }
    }
}