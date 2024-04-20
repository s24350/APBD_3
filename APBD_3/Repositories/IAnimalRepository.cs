using APBD_3.Models;

namespace APBD_3.Repositories
{
    public interface IAnimalRepository
    {
        public IEnumerable<Animal> GetAnimals(string orderBy);
    }
}
