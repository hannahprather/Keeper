using System;
using System.Collections.Generic;
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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeepsController>> Create([FromBody] VaultKeep vkData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vkData.CreatorId = userInfo.Id;
        VaultKeep created = _vks.Create(vkData);
        return Ok(created);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    // [HttpGet("{id}")]
    // public ActionResult<List<VaultKeep>> GetKeepByVaultId(int id)
    // {
    //   try
    //   {
    //     return Ok(_vks.GetKeepsByVaultId(id));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpGet("{id}")]
    public ActionResult<List<VaultKeep>> GetKeepById(int id)
    {
      try
      {
        return Ok(_vks.GetKeepById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vks.DeleteVaultKeep(id, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}