using DTOs.AccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obj_Common;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HowToPlay()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]        
        public async Task<IActionResult> Login(Login login)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(Route.Login, login);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("ErrorMessage", "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
                return View(login);
            }

            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Tokens tokens = JsonSerializer.Deserialize<Tokens>(strData, options);

            var CookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true
            };

            Response.Cookies.Append("access", tokens.Access_Token, CookieOptions);
            Response.Cookies.Append("refresh", tokens.Refresh_Token, CookieOptions);
            Response.Cookies.Append("isLogin", "true", CookieOptions);

            return RedirectToAction("Index", "HomePlayer", new { area = "Player" });
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(Route.Register, register);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("ErrorMessage", "Đăng kí không thành công.");
                return View(register);
            }
            
            return RedirectToAction(nameof(Login));
        }
    }
}
