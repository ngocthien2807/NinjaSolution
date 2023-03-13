using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using DTOs.CharacterDTOs;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Obj_Common;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Web_Client.Areas.Admin.Controllers
{
    public class CharacterAdminController : Controller
    {
        private readonly HttpClient client = null;

        public static IWebHostEnvironment _hostingEnvironment;

        public CharacterAdminController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
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
            List<ViewCharacterAdmin> characters = JsonSerializer.Deserialize<List<ViewCharacterAdmin>>(strData, options);
            
            return View(characters);
        }

        public IActionResult Add()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try {
                string path = "Upload/";

                //get abs path of wwwroot
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, path);

                string relPath = null;

                //if not exists path, create it
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }


                if (image != null && image.Length > 0)
                {


                    //rename file b_Cover by b_ID
                    string namefile = image.FileName;

                    // absPath/fileName

                    string absPathCover = Path.Combine(uploads, namefile);


                    using (var stream = System.IO.File.Create(absPathCover))
                    {
                        //Copy file to path
                        await image.CopyToAsync(stream);
                    }

                    // relPath to store database
                    relPath = "/" + path + "/" + namefile;
                }

                return Json(new { status = "success", path = relPath });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });

            }
        }


     
        public async Task<ViewCharacterInfo> GetByID(string id)
        {
            string access = Request.Cookies["access"];
            string refresh = Request.Cookies["refresh"];

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access);
            client.DefaultRequestHeaders.Add("refresh", refresh);


            HttpResponseMessage response = await client.GetAsync(String.Format(Route.getByIDCharacter, id));
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ViewCharacterInfo characters = JsonSerializer.Deserialize<ViewCharacterInfo>(strData, options);

            return characters;
        }

        public async Task<IActionResult> Detail(string id)
        {
            var character = await GetByID(id);
            return View(character);
        }

        public async Task<IActionResult> Update(string id)
        {
            var character = await GetByID(id);
            return View(character);
        }

    }
}
