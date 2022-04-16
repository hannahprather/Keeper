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

    internal Keep Get(int id)
    {
      return _kRepo.Get(id);
    }
  }
}