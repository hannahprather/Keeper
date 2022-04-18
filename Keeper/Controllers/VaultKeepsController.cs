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



    // [HttpDelete("{id}")]
    // [Authorize]

  }
}