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

namespace GameRating.Controllers
{
    public class GamesController : Controller
    {
        public GamesController()
        { }
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44397/api/games/getallgamedetails/");
            if(response.IsSuccessStatusCode)   
            {
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<GameDetailsDto>>>(unResult);
                var carouselResult = result.Data.OrderByDescending(g => g.Rate).Take(5);
                return View(carouselResult);
            }
            return BadRequest();
        }
        [Route("/Rate/{id}")]
        public async Task<IActionResult> Rate(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44397/api/games/getgamedetailsbyid/?id=" + id);
            string unResult = response.Content.ReadAsStringAsync().Result;
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<GameDetailsDto>>(unResult);
            response= await client.GetAsync("https://localhost:44397/api/ratings/getratingbygameid/?id=" + id);
            unResult = response.Content.ReadAsStringAsync().Result;
            var ratingResult= Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<RatingDetailsDto>>>(unResult);
            ViewBag.Ratings = ratingResult.Data;
            return View(result.Data);
        }
        public async Task<IActionResult> Search(string name)
        {
            /*if(Request.Cookies["token"]!=null)
            {*/
                /*var json = Newtonsoft.Json.JsonConvert.SerializeObject(name);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");*/
                var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
                var response = await client.GetAsync("https://localhost:44397/api/games/getgamedetailsbyname/?name="+name);
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<GameDetailsDto>>>(unResult);
                return View(result.Data);
            //} Yetkilendirme yapıldı
            //return RedirectToAction("Index");
        }
    }
}
