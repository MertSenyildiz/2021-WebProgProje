using Core.Entities.Concrete;
using Entities.Concrete;
using GameRating.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GameRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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


        public IActionResult Logout()
        {
            if (Request.Cookies["token"] != null)
            {
                Response.Cookies.Delete("token");
            }
            return RedirectToAction("Index","Games");
        }
        public async Task<IActionResult> AdminPanelAsync()
        {
            if(Request.Cookies["token"]!=null)
            {
                var token = Request.Cookies["token"];
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var jti = jwtSecurityToken.Claims;//.Where(c=>c.Type== "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                var user = new User { ID = Convert.ToInt32(jwtSecurityToken.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value),UserName= jwtSecurityToken.Claims.First(c => c.Type == "unique_name").Value };
                if(jti.Where(c=>c.Value=="Admin").Any())
                {
                    using(HttpClient client=new HttpClient()) {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
                        var response = await client.GetAsync("https://localhost:44397/api/publishers/getall");
                        string unResult = response.Content.ReadAsStringAsync().Result;
                        var resultPublisher = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<Publisher>>>(unResult);
                        ViewData["PublisherID"] = new SelectList(resultPublisher.Data, "ID", "PublisherName");
                        response = await client.GetAsync("https://localhost:44397/api/developers/getall");
                        unResult = response.Content.ReadAsStringAsync().Result;
                        var resultDeveloper = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<Developer>>>(unResult);
                        ViewData["DeveloperID"] = new SelectList(resultDeveloper.Data, "ID", "DeveloperName");
                        response = await client.GetAsync("https://localhost:44397/api/categories/getall");
                        unResult = response.Content.ReadAsStringAsync().Result;
                        var resultCategory = Newtonsoft.Json.JsonConvert.DeserializeObject<DataResult<List<Category>>>(unResult);
                        ViewData["CategoryID"] = new SelectList(resultCategory.Data, "ID", "CategoryName");
                        return View(user);
                    }  
                }
            }
            return RedirectToAction("UnAuth");
        }

        [HttpPost]
        public IActionResult LanguageAdminPanel(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("AdminPanel");
        }

        public IActionResult Login()
        {
            if (Request.Cookies["token"] != null)
            {
                return RedirectToAction("Index","Games");
            }
            return View();
        }

        [HttpPost]
        public IActionResult LanguageLogin(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("Login");
        }

        public IActionResult UnAuth()
        {
            return View();
        }
        public IActionResult Register()
        {
            if (Request.Cookies["token"] != null)
            {
                return RedirectToAction("Index", "Games");
            }
            return View();
        }

        
        [HttpPost]
        public IActionResult LanguageRegister(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );


            return RedirectToAction("Register");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
