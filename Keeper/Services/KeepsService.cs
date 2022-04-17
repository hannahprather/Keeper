using System;
using System.Collections.Generic;
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
      return _kRepo.GetById(id);
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
  }
}