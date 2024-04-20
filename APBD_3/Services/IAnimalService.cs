using APBD_3.DTO;
using APBD_3.Models;

namespace APBD_3.Services
{
    public interface IAnimalService
    {
        public IEnumerable<AnimalDTO> GetAnimals(string orderBy);
        public int PostAnimal(AnimalDTO animalDTO);
    }
}
