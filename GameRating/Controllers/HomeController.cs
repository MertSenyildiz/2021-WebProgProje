using Core.Entities.Concrete;
using GameRating.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public IActionResult AdminPanel()
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
                    return View(user);
                }
                else
                {
                    var item=HttpContext.Request.Cookies["token"];
                    return Unauthorized();
                    
                }
            }
            return Unauthorized("Yetkisiz");
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
