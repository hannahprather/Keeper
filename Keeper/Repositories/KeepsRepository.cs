using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }


    public Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, kept, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    public List<Keep> GetAllKeeps()
    {
      string sql = @"
      SELECT 
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }).ToList();
    }

    public Keep GetById(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (kp, prof) =>
      {
        kp.Creator = prof;
        return kp;
      }, new { id }).FirstOrDefault();
    }

    internal Keep Update(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id;
      ";
      int rows = _db.Execute(sql, original);
      if (rows > 0)
      {
        return original;
      }
      throw new Exception("Sql error ham!!!! no updates");
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Keep> GetKeepsByAccount(string accountId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @accountId;";
      return _db.Query<Keep, Account, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { accountId }, splitOn: "id");
    }

    internal void IncreaseViews(int id)
    {
      string sql = @"
      UPDATE keeps
      SET views = views + 1
      WHERE id = @id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}