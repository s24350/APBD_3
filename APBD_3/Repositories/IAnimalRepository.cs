using APBD_3.Models;

namespace APBD_3.Repositories
{
    public interface IAnimalRepository
    {
        public IEnumerable<Animal> GetAnimals(string orderBy);

        public int GetMaxId();

        public int PostAnimal(Animal animal);

        public Animal GetAnimalById(int idAnimal);

        public int UpdateAnimal(Animal animal);
        public int DeleteAnimal(int idAnimal);

        public List<int> GetExistingIds();
    }
}
