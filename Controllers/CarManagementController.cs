using Microsoft.AspNetCore.Mvc;
using WebApiCar.Services;

namespace WebApiCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarManagementController
    {
        private readonly ICarService _carService;

        public CarManagementController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [Route("cars/{carId}")]
        public async Task<ActionResult> GetCar([FromRoute] int carId)
        {
            var car = _carService.

        }
    }
}
