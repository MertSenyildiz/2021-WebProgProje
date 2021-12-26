using Business.Abstract;
using Entities.Concrete;
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
    public class RatingsController : ControllerBase
    {
        IRatingService _ratingService;
        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("addrating")]
        public IActionResult AddRating(Rating rating)
        {
            var result = _ratingService.Add(rating);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getratingbygameid")]
        public IActionResult GetRatingByGameId(int id)
        {
            var result = _ratingService.GetAllRatingDetailsByGameId(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("checkrateexist")]
        public IActionResult CheckRateExist(Rating rating)
        {
            var result = _ratingService.CheckRateExist(rating);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
