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
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountservice;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public ProfilesController(AccountService accountservice, KeepsService ks, VaultsService vs)
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

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<Account>> GetKeepsByAccount(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ks.GetKeepsByAccount(id, userInfo?.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<Account>> GetVaultsByAccount(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.GetVaultsByAccount(id, userInfo?.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}")]
    public async Task<ActionResult<Account>> GetProfileById(string accountId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountservice.GetProfileById(accountId));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
  }
}
