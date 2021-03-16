using Microsoft.AspNetCore.Mvc;
using ZooManagement.Models.Database;
using ZooManagement.Models.Request;
using ZooManagement.Models.Response;
using ZooManagement.Repositories;
using System;
using System.Text;

namespace ZooManagement.Controllers 
{
    [ApiController]
    [Route("/animals")]
    public class AnimalController : ControllerBase 
    {
        private readonly IAnimalsRepo _animals;

        public AnimalController(IAnimalsRepo animals)
        {
            _animals = animals;
        }

        [HttpGet("{id}")]
        public ActionResult<AnimalResponse> GetById([FromRoute] int id)
        {
            var animal = _animals.GetById(id);
            return new AnimalResponse(animal);
        }

        [HttpPost("add")]
        public ActionResult<Animal> Add([FromBody] AddAnimalRequest newAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var animal = _animals.Add(newAnimal);

            // var url = Url.Action("GetById", new { id = animal.Id });
            // var responseViewModel = new AnimalResponse(animal);
            // return Created(url, responseViewModel);
            return animal;
        }
    }
}