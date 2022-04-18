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
    
  }
}