using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System;
using AppServices.CharacterRepo;

namespace API_Common.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        CharacterRepositories CharacterManager;

        public CharactersController(CharacterRepositories characterManager)
        {
            CharacterManager = characterManager;
        }

        [HttpGet]
        public IActionResult GetAllCharacter()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, CharacterManager.GetAllCharacter());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet("{characterId}")]
        public IActionResult GetCharacterbyID(string characterId)
        {
            try
            {
                var character = CharacterManager.GetCharacterbyID(characterId);
                if (character == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Monster does not exists");
                return StatusCode((int)HttpStatusCode.OK, character);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
