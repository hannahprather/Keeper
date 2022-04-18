using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfileController : ControllerBase
  {
    private readonly AccountService _accountservice;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public ProfileController(AccountService accountservice, KeepsService ks, VaultsService vs)
    {
      _accountservice = accountservice;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<AccountController>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountservice.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    // [HttpGet("{id}/keeps")]
    // public async Task<ActionResult<Account>> GetMyKeeps(string id)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     return Ok(_ks.GetMyKeeps(id, userInfo?.Id));


    //   }
    //   catch (Exception e)
    //   {

    //     return BadRequest(e.Message);
    //   }

  }
}
