using System;
using System.Collections.Generic;
using System.Linq;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vRepo;
    private readonly AccountsRepository _repo;
    private readonly VaultKeepsRepository _vkRepo;

    public VaultsService(VaultsRepository vRepo, AccountsRepository repo, VaultKeepsRepository vkRepo)
    {
      _vRepo = vRepo;
      _repo = repo;
      _vkRepo = vkRepo;
    }

    internal Vault Create(Vault vaultData)
    {
      vaultData.Id = _vRepo.Create(vaultData);
      return vaultData;
    }


    //TODO fix all these if statements bro.. this code looks hella messy
    internal Vault GetVaultById(int id, string userId)
    {
      Vault foundVault = _vRepo.GetVaultById(id);
      Account creator = _repo.GetById(foundVault.CreatorId);
      foundVault.Creator = creator;

      if (foundVault == null)
      {
        throw new Exception("this vault be null babby");
      }
      if (foundVault.IsPrivate == false)
      {
        return foundVault;
      }
      if (foundVault.CreatorId == userId && foundVault.IsPrivate == true)
      {
        return foundVault;
      }
      if (foundVault.CreatorId != userId && foundVault.IsPrivate == true)
      {
        throw new Exception("no chance buddy, this vault is private *shoots gun.. blows off smoke... twirls gun... and holsters");
      }

      return foundVault;
    }

    internal Vault Edit(Account userInfo, Vault vaultData)
    {
      Vault origional = _vRepo.GetVaultById(vaultData.Id);
      if (origional == null)
      {
        throw new Exception("this vault is null playyyyyaaaa");
      }
      if (origional.CreatorId != userInfo.Id)
      {
        throw new Exception("IMPOSTER!!!!!");
      }
      _vRepo.Edit(vaultData);
      return _vRepo.GetVaultById(vaultData.Id);
    }

    internal string Delete(int id, Account userInfo)
    {
      Vault vault = _vRepo.GetVaultById(id);
      if (vault == null)
      {
        throw new Exception("this vault be null my guy");
      }
      if (vault.CreatorId != userInfo.Id)
      {
        throw new Exception("not yours to delort my friend, go delort something you made instead");
      }
      if (_vRepo.Delete(id))
      {
        return "deleted";
      }
      else
      {
        return "couldnt make it delorted";
      }

    }
    internal object GetVaultsByAccount(string accountId, string userId)
    {
      return _vRepo.GetVaultsByAccount(accountId).ToList().FindAll(v => v.CreatorId == userId || v.CreatorId == accountId && v.IsPrivate == false);
    }

    public List<VaultKeepViewModel> GetKeepsByVaultId(int id, string userId)
    {
      Vault vault = GetVaultById(id, userId);
      return _vkRepo.GetKeepsByVaultId(id);
    }


    internal List<Vault> GetMyVaults(Account userInfo)
    {
      List<Vault> myVaults = _vRepo.GetMyVaults(userInfo.Id);
      return myVaults;
    }
  }

}