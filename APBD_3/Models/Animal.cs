using System.ComponentModel.DataAnnotations;

namespace APBD_3.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
    }
}
