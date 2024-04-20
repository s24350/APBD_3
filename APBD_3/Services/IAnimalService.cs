using APBD_3.DTO;

namespace APBD_3.Services
{
    public interface IAnimalService
    {
        public IEnumerable<AnimalDTO> GetAnimals(string orderBy);
    }
}
