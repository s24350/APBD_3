using APBD_3.DTO;
using APBD_3.Repositories;

namespace APBD_3.Services
{
    // service is to communicate with repository
    // Animal <-> Animal DTO change occures here
    // [controller] <-> [service] <-> [repository] <-> [database]
    public class AnimalService : IAnimalService
    {

        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository) {
            this._animalRepository = animalRepository;
        }

        public IEnumerable<AnimalDTO> GetAnimals(string orderBy)
        {
            var animalsFromDatabase = _animalRepository.GetAnimals(orderBy);
            var animalsForController = new List<AnimalDTO>();

            foreach (var animal in animalsFromDatabase)
            {
                AnimalDTO animalDTO = new()
                {
                    IdAnimal = animal.IdAnimal,
                    Name = animal.Name,
                    Description = animal.Description,
                    Area = animal.Area,
                    Category = animal.Category
                };
                animalsForController.Add(animalDTO);
            }
            //sorting by column occures here
            return animalsForController.OrderBy(animal=>animal.GetType().GetProperty(orderBy).GetValue(animal));
        }


    }
}
