using REST_API.Models;
using REST_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService) =>
            _carService = carService;

        [HttpGet]
        public async Task<List<Car>> Get() =>
            await _carService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await _carService.GetAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Car newCar)
        {
            await _carService.CreateAsync(newCar);

            return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            var user = await _carService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

           // updatedCar.Id = user.Id;

            await _carService.UpdateAsync(id, updatedCar);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _carService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _carService.RemoveAsync(id);

            return NoContent();
        }
    }
}

