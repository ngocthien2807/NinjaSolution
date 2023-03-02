using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Obj_Common;

namespace Web_Client.Controllers
{
    public class AbilityController : Controller
    {
        private readonly HttpClient client = null;

        public AbilityController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.getAllAbility);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Ability> bosses = JsonSerializer.Deserialize<List<Ability>>(strData, options);
            return View(bosses);
        }

        public async Task<IActionResult> GetbyID(string Id)
        {
            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDAbility, Id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Ability ability = JsonSerializer.Deserialize<Ability>(strData, options);
            return View(ability);
        }
    }
}
