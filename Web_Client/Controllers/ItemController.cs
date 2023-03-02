using DataAccess.Models;
using DTOs.ItemDTOs;
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
    public class ItemController : Controller
    {
        private readonly HttpClient client = null;

        public ItemController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.getAllItem);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ViewItem> items = JsonSerializer.Deserialize<List<ViewItem>>(strData, options);
            return View(items);
        }

        public async Task<IActionResult> GetbyID(string Id)
        {
            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDItem, Id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ViewItem item = JsonSerializer.Deserialize<ViewItem>(strData, options);
            return View(item);
        }
    }
}
