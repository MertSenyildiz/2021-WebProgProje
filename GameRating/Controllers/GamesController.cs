using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using GameRating.Models;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Core.Entities.Concrete;


namespace GameRating.Controllers
{
    public class GamesController : Controller
    {
        HttpClient _client;
        public GamesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44397/api/games/");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("getallgamedetails/");
            if(response.IsSuccessStatusCode)   
            {
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<GameDetailsDto>>>(unResult);
                var carouselResult = result.Data.OrderByDescending(g => g.Rate).Take(5);
                return View(carouselResult);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult LanguageGames(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("Index", "Games");
        }


        [Route("/Rate/{id}")]
        public async Task<IActionResult> Rate(int id)
        {
            using(var client=new HttpClient())
            {
                var response = await _client.GetAsync("getgamedetailsbyid/?id=" + id);
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<GameDetailsDto>>(unResult);
                response = await client.GetAsync("https://localhost:44397/api/ratings/getratingbygameid/?id=" + id);
                unResult = response.Content.ReadAsStringAsync().Result;
                var ratingResult = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<RatingDetailsDto>>>(unResult);
                ViewBag.Ratings = ratingResult.Data;
                return View(result.Data);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> RateGame()
        {
            var rating = new Rating {
                GameID= Convert.ToInt32(HttpContext.Request.Form["GameID"]),
                UserID= Convert.ToInt32(HttpContext.Request.Form["UserID"]),
                Rate = Convert.ToInt32(HttpContext.Request.Form["Rate"]),
                Comment = HttpContext.Request.Form["Comment"],
                Date=DateTime.Now
            };
            if (Request.Cookies["token"] != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(rating);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44397/api/ratings/checkrateexist/",data);
                    string unResult = response.Content.ReadAsStringAsync().Result;
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<bool>>(unResult);
                    if (!result.Data)
                        response = await client.PostAsync("https://localhost:44397/api/ratings/addrating/", data);
                    if (response.IsSuccessStatusCode)
                    {
                            return RedirectToAction("Rate", new { id = rating.GameID });
                    }
                    else
                    {
                        ViewData["Error"] = "Error";
                        return RedirectToAction("Rate", new { id = rating.GameID });
                    }
                }
            }
            return RedirectToAction("UnAuth","Home");

        }

        [HttpPost]
        public IActionResult LanguageRate(string culture, int gameId)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("Rate", new { id = gameId });
        }


        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var game = new Game
            {
                GameName= HttpContext.Request.Form["GameName"],
                DeveloperID = Convert.ToInt32(HttpContext.Request.Form["DeveloperID"]),
                PublisherID = Convert.ToInt32(HttpContext.Request.Form["PublisherID"]),
                CategoryID = Convert.ToInt32(HttpContext.Request.Form["CategoryID"]),
                ReleaseDate= Convert.ToDateTime(HttpContext.Request.Form["ReleaseDate"])
            };
            if (Request.Cookies["token"] != null)
            {
                var token = Request.Cookies["token"];
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var jti = jwtSecurityToken.Claims;//.Where(c=>c.Type== "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                var user = new User { ID = Convert.ToInt32(jwtSecurityToken.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value), UserName = jwtSecurityToken.Claims.First(c => c.Type == "unique_name").Value };
                if (jti.Where(c => c.Value == "Admin").Any())
                {
                    using(HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(game);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("https://localhost:44397/api/games/add/", data);
                        return RedirectToAction("AdminPanel", "Home");
                    }
                        
                }
            }
            return RedirectToAction("UnAuth", "Home");
        }
        public async Task<IActionResult> Search(string name)
        {
            /*if(Request.Cookies["token"]!=null)
            {*/
                /*var json = Newtonsoft.Json.JsonConvert.SerializeObject(name);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");*/
                //client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
                var response = await _client.GetAsync("getgamedetailsbyname/?name="+name);
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<GameDetailsDto>>>(unResult);
                return View(result.Data);
            //} Yetkilendirme yapıldı
            //return RedirectToAction("Index");
        }
        public async Task<IActionResult> ListGames()
        {
            var response = await _client.GetAsync("getallgamedetails/");
            string unResult = response.Content.ReadAsStringAsync().Result;
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<GameDetailsDto>>>(unResult);
            return View(result.Data);
        }
    }
}
