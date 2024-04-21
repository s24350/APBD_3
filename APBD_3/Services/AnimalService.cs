using APBD_3.DTO;
using APBD_3.Enums;
using APBD_3.Models;
using APBD_3.Repositories;
using APBD_3.Validators;
using Microsoft.AspNetCore.Mvc;

namespace APBD_3.Services
{
    // service is to communicate with repository
    // Animal <-> Animal DTO change occures here
    // [controller] <-> [service] <-> [repository] <-> [database]
    public class AnimalService : IAnimalService
    {

        private readonly IAnimalRepository _animalRepository;
        private IIndexValidator _indexValidator;

        public AnimalService(IAnimalRepository animalRepository, IIndexValidator indexValidator) {
            this._animalRepository = animalRepository;
            this._indexValidator = indexValidator;
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

        public int PostAnimal(AnimalDTO animalDTO, PostType postType)
        {
            int adder = 0;
            if (postType == PostType.postNew)
            {
                adder++;
            }
            Animal animal = new()
            {
                IdAnimal = (_animalRepository.GetMaxId()+adder),
                Name = animalDTO.Name,
                Description = animalDTO.Description,
                Area = animalDTO.Area,
                Category = animalDTO.Category
            };
            return _animalRepository.PostAnimal(animal);
        }

        public int UpdateAnimal(int idAnimal, AnimalDTO animalDTO)
        {
            if (!_animalRepository.GetExistingIds().Contains(idAnimal)){
                return 0;
            }
            
            Animal animalForDatabase = new()
            {
                IdAnimal = idAnimal,
                Name = animalDTO.Name,
                Description = animalDTO.Description,
                Area = animalDTO.Area,
                Category = animalDTO.Category
            };
            return _animalRepository.UpdateAnimal(animalForDatabase);
        }

        public int DeleteAnimal(int idAnimal)
        {
            
            if (_indexValidator.IsIndexInAnimalsTable(idAnimal, _animalRepository.GetExistingIds())){
                return _animalRepository.DeleteAnimal(idAnimal);
            }
            return 0;
        }
    }
}
