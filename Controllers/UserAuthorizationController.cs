using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCar.Models.Dtos;

namespace WebApiCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthorizationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto userRegisterDto)
        {

        }
    }
}
