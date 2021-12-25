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
    public class GamesController : ControllerBase
    {
        readonly private IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result=_gameService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getgamedetailsbyid")]
        public IActionResult GetGameDetailsById(int id)
        {
            var result = _gameService.GetGameDetailsById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getgamedetailsbyname")]
        public IActionResult GetGameDetailsByName(string name)
        {
            var result = _gameService.GetGameDetailsByName(name);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getallgamedetails")]
        public IActionResult GetAllGameDetails()
        {
            var result = _gameService.GetAllGameDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Game game)
        {
            var result = _gameService.Add(game);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Game game)
        {
            var result = _gameService.Update(game);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Game game)
        {
            var result = _gameService.Delete(game);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
