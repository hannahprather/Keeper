using System;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkRepo;
    private readonly VaultsRepository _vRepo;
    public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vRepo)
    {
      _vkRepo = vkRepo;
      _vRepo = vRepo;
    }

    public VaultKeep Create(VaultKeep vkData)
    {
      Vault created = _vRepo.GetVaultById(vkData.VaultId);
      if (created.CreatorId != vkData.CreatorId)
      {
        throw new Exception("nooo surrrr, not today");
      }
      vkData.Id = _vkRepo.Create(vkData);
      return vkData;
    }

    internal object GetKeepsByVaultId(int id)
    {
      return _vkRepo.GetKeepsByVaultId(id);
    }

    internal VaultKeep GetKeepById(int id)
    {
      return _vkRepo.GetKeepById(id);
    }



    internal string DeleteVaultKeep(int id, Account userInfo)
    {
      VaultKeep remove = _vkRepo.GetKeepById(id);
      if (remove == null)
      {
        throw new Exception("not a real id my guuuy");
      }
      else if (remove.CreatorId != userInfo.Id)
      {
        throw new Exception("HANDS OFF!!! NO TOUCHING WHAT ISNT YOURS");
      }
      else if (_vkRepo.DeleteVaultKeep(id))
      {
        return "Delorted";
      }
      else
      {
        return "Error could not be delorted";
      }
    }
  }
}