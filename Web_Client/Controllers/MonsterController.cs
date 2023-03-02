using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Obj_Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_Client.Controllers
{
    public class MonsterController : Controller
    {
        private readonly HttpClient client = null;

        public MonsterController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.getAllMonster);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Monster> monsters = JsonSerializer.Deserialize<List<Monster>>(strData, options);
            return View(monsters);
        }

        public async Task<IActionResult> GetbyID(string Id)
        {
            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDMonster, Id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Monster monster = JsonSerializer.Deserialize<Monster>(strData, options);
            return View(monster);
        }
    }
}
