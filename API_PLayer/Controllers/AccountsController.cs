using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using AppServices.AccountRepo;
using DTOs.AccountDTOs;
using DataAccess.Models;
using Obj_Common;
using Microsoft.AspNetCore.Authorization;

namespace API_Player.Controllers
{
    [Route("api/Player/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        IMapper mapper;
        AccountRepositories AccountManager;

        public AccountsController(IMapper mapper, AccountRepositories accountManager)
        {
            this.mapper = mapper;
            AccountManager = accountManager;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                string id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;
                var account = AccountManager.Profile(int.Parse(id));

                if (account == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Account does not exists.");

                return StatusCode((int)HttpStatusCode.OK, account);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProfile(UpdateProfile updateAccount)
        {
            try
            {
                string id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;

                var  account = mapper.Map<UpdateProfile, Account>(updateAccount);

                account.AccountId = int.Parse(id);

                if (!AccountManager.UpdateProfile(account))
                    return StatusCode((int)HttpStatusCode.BadRequest, "Account does not exists.");

                return StatusCode((int)HttpStatusCode.OK, "Update profile success");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateGameSpecs(UpdateGameSpecs updateAccount)
        {
            try
            {
                string id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;

                var account = mapper.Map<UpdateGameSpecs, Account>(updateAccount);

                account.AccountId = int.Parse(id);

                if (!AccountManager.UpdateGameSpecs(account))
                    return StatusCode((int)HttpStatusCode.BadRequest, "Account does not exists.");

                return StatusCode((int)HttpStatusCode.OK, "Update profile success");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

    }
}
