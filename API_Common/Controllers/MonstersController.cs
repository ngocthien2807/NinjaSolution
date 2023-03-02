using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using AppServices.MonsterRepo;

namespace API_Common.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MonstersController : ControllerBase
    {
        MonsterRepositories MonsterManager;

        public MonstersController(MonsterRepositories monsterManager)
        {
            MonsterManager = monsterManager;
        }

        [HttpGet]
        public IActionResult GetAllMonster()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, MonsterManager.GetAllMonster());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet("{monsterId}")]
        public IActionResult GetMonsterbyID(string monsterId)
        {
            try
            {
                var monster = MonsterManager.GetMonsterbyID(monsterId);
                if (monster == null) 
                    return StatusCode((int)HttpStatusCode.BadRequest, "Monster does not exists");
                return StatusCode((int)HttpStatusCode.OK, monster);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
