using GameRating.Models;
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
        public IActionResult Privacy()
        {
            if(Request.Cookies["token"]!=null)
            {
                var token = Request.Cookies["token"];
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var jti = jwtSecurityToken.Claims.Where(c=>c.Type== "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                if(jti.Where(c=>c.Value=="Admin").Any())
                {
                    return Ok("Merhaba");
                }
                else
                {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
