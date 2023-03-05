using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Obj_Common;
using DTOs.CharacterDTOs;

namespace Web_Client.Controllers
{
    public class CharacterController : Controller
    {
        private readonly HttpClient client = null;

        public CharacterController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(Route.getAllCharacter);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ViewCharacter> characters = JsonSerializer.Deserialize<List<ViewCharacter>>(strData, options);
            return View(characters);
        }

        public async Task<IActionResult> GetbyID(string Id)
        {
            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDCharacter, Id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ViewCharacterInfo character = JsonSerializer.Deserialize<ViewCharacterInfo>(strData, options);
            return View(character);
        }
    }
}
