using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _publisherService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
