using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgProje.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IRatingService _ratingService;
        public GamesController(IGameService gameService, IRatingService ratingService)
        {
            _gameService = gameService;
            _ratingService = ratingService;
        }
        [Route("/Games")]
        public IActionResult Index()
        {
            var result = _gameService.GetAllGameDetails();
            var result2 = result.Data.OrderByDescending(g => g.Rate).Take(5);//1 tane aldı;
            return View(result2);
        }

        public IActionResult Rating()
        {
            return View();
        }

        [Route("/Game/{id}")]
        public IActionResult Game(int id)
        {
            var result = _gameService.GetGameDetailsById(id);
            var ratingResult = _ratingService.GetAllRatingDetailsByGameId(id);
            ViewBag.Ratings= ratingResult.Data;
            return View(result.Data);
        }

    }
}
