using APBD_3.Models;

namespace APBD_3.Repositories
{
    public interface IAnimalRepository
    {
        public IEnumerable<Animal> GetAnimals(string orderBy);

        public int GetCount();

        public int PostAnimal(Animal animal);
    }
}
