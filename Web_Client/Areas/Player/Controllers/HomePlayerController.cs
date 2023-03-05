using DTOs.AccountDTOs;
using Microsoft.AspNetCore.Mvc;
using Obj_Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_Client.Areas.Player.Controllers
{
    [Area("Player")]
    [Route("Player/HomePlayer")]

    public class HomePlayerController : Controller
    {
        private readonly HttpClient client = null;

        public HomePlayerController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.Header);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");

            }

            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            AccountHeader accountHeader = JsonSerializer.Deserialize<AccountHeader>(strData, options);

          
            return View(accountHeader);
        }
    }
}
