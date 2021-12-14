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
