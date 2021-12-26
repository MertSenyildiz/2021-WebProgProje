using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        IDeveloperService _developerService;
        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _developerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
