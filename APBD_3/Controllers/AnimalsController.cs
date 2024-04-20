using APBD_3.Repositories;
using APBD_3.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_3.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            this._animalService = animalService;
        }

        [HttpGet]
        public IActionResult GetOrderedAnimals(string orderBy = "Name")
        {
            return Ok(_animalService.GetAnimals(orderBy));
        }
    }
}
