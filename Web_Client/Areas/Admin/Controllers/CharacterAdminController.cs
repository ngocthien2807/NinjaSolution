using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using DTOs.CharacterDTOs;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Obj_Common;

namespace Web_Client.Areas.Admin.Controllers
{
    public class CharacterAdminController : Controller
    {
        private readonly HttpClient client = null;

        public CharacterAdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            string access = Request.Cookies["access"];
            string refresh = Request.Cookies["refresh"];

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access);
            client.DefaultRequestHeaders.Add("refresh", refresh);


            HttpResponseMessage response = await client.GetAsync(Route.getAllCharacterAdmin);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ViewCharacterInfo> characters = JsonSerializer.Deserialize<List<ViewCharacterInfo>>(strData, options);
            
            return View(characters);
        }
    }
}
