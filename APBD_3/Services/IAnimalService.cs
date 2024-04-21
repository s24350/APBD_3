using APBD_3.DTO;
using APBD_3.Enums;
using APBD_3.Models;

namespace APBD_3.Services
{
    public interface IAnimalService
    {
        public IEnumerable<AnimalDTO> GetAnimals(string orderBy);
        public int PostAnimal(AnimalDTO animalDTO, PostType postType);

        public int UpdateAnimal(int idAnimal, AnimalDTO animalDTO);

        public int DeleteAnimal(int idAnimal);
    }
}
