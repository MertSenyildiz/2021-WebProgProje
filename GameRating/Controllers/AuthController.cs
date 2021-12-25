using Business.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameRating.Controllers
{
    public class AuthController : Controller
    {

            private IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }
            
            [HttpPost("login")]
            public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(userForLoginDto);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync("https://localhost:44397/api/auth/login", data);
                if(response.IsSuccessStatusCode)
                {
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(unResult);
                CookieOptions cookie=new CookieOptions();
                cookie.HttpOnly = true;
                cookie.Expires = result.Expiration;
                Response.Cookies.Append("token",result.Token,cookie);
                return RedirectToAction("Index", "Games");
            }
            return RedirectToAction("Login","Home");
                
            }
            [HttpPost("register")]
            public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(userForRegisterDto);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync("https://localhost:44397/api/auth/register", data);
                string unResult = response.Content.ReadAsStringAsync().Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(unResult);
                return Ok(result.Token);
            //TODO:Düzenle
            }
        }
}
