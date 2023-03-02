using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System;
using AppServices.MissionRepo;
using DTOs.MissionDTOs;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace API_Player.Controllers
{
    [Route("api/Player/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MissionsController : ControllerBase
    {
        MissionRepositories MissionManager;

        public MissionsController(MissionRepositories missionManager)
        {
            MissionManager = missionManager;
        }

        [HttpGet]
        public IActionResult GetMissionbyAccount()
        {
            try
            {
                string id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;

                List<ViewMission> missions = MissionManager.GetMissionbyAccount(int.Parse(id));

                if (missions == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Mission does not exists.");
                return StatusCode((int)HttpStatusCode.OK, missions);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
