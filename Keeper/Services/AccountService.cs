using System;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class AccountService
  {
    private readonly AccountsRepository _repo;
    public AccountService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }

    internal object GetById(string accountId)
    {
      Account account = _repo.GetById(accountId);
      if (account == null)
      {
        throw new Exception("oi.. this account aint it");
      }
      return account;
    }
    internal object GetProfileById(string accountId)
    {
      Account profile = _repo.GetProfileById(accountId);
      if (profile == null)
      {
        throw new Exception("you do not exist.. we are all in a simulation");
      }
      return profile;

    }

    //  internal object GetMyVaults(Account userInfo)
    //  {
    //    Account vaults = _repo.GetMyVaults(userInfo);
    //    if 
    //     {
    //       throw new Exception("does not exist doggie");
    //     }
    //     return vaults;
    //   }
  }
}
