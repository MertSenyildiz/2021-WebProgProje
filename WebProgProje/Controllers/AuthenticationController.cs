using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebProgProje.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            
            return View();
        }
        public async Task<IActionResult> GirdiAsync(UserForLoginDto dto)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44397/api/games/getall");
            HttpResponseMessage response = await client.SendAsync(request);
            string deneme = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AccessToken>(deneme);
            ViewBag.Deneme = result;
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
