﻿using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebProgProje.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WebProgProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;
        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Cookie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cookie(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );
            

            return RedirectToAction("Cookie");
        }

        [HttpPost]
        public IActionResult Index(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            /*HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44397/api/games/getall");
            HttpResponseMessage response = await client.SendAsync(request);
            string deneme = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Deneme<List<Game>>>(deneme);
            //ViewBag.Deneme = result.Data;*/
            var result = _gameService.GetAllGameDetails();
            var result2 = result.Data.OrderByDescending(g => g.Rate).Take(1);//1 tane aldı;
            return View(result2);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
