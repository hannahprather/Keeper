using System;
using System.Collections.Generic;
using System.Linq;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kRepo;
    public KeepsService(KeepsRepository kRepo)
    {
      _kRepo = kRepo;
    }

    internal Keep Create(Keep keepData)
    {
      return _kRepo.Create(keepData);
    }

    internal List<Keep> GetAllKeeps()
    {
      return _kRepo.GetAllKeeps();
    }

    internal Keep GetById(int id)
    {
      Keep found = _kRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("does not exist doggie");
      }
      return found;
    }

    internal Keep Update(Keep updateData)
    {
      Keep original = GetById(updateData.Id);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("Cannot Edit, Not your restaurant");
      }
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      return _kRepo.Update(original);
    }

    internal void Delete(int id, string userId)
    {
      Keep original = GetById(id);
      if (userId == original.CreatorId)
      {
        _kRepo.Delete(id);
      }
      else
      {
        throw new Exception("invalid");
      }
    }

    // internal List<Keep> GetMyKeeps(string accountId, string userId)
    // {
    //   return _kRepo.GetMyKeeps(accountId).ToList().FindAll(k => k.CreatorId == userId || accountId);
    // }
  }
}