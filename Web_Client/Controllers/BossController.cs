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
    public class BossController : Controller
    {
        private readonly HttpClient client = null;

        public BossController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.getAllBoss);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Boss> bosses = JsonSerializer.Deserialize<List<Boss>>(strData, options);
            return View(bosses);
        }

        public async Task<IActionResult> GetbyID(string Id)
        {
            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDBoss, Id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Boss boss = JsonSerializer.Deserialize<Boss>(strData, options);
            return View(boss);
        }
    }
}
