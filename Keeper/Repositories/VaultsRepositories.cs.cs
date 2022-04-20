using System.Collections.Generic;
using System.Data;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }


    internal int Create(Vault vaultData)
    {
      string sql = @"
    INSERT INTO vaults
    (id, creatorId, name, description, isPrivate)
    VALUES
    (@Id, @CreatorId, @Name, @Description, @IsPrivate);
    SELECT LAST_INSERT_ID();
    ";
      return _db.ExecuteScalar<int>(sql, vaultData);
    }

    internal Vault GetVaultById(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    //NOTE ask about the immage on the tests... its still passing but why is it there if it isnt in out model
    internal void Edit(Vault vaultData)
    {
      string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    description = @Description,
    isPrivate = @IsPrivate";
      _db.ExecuteScalar(sql, vaultData);
    }

    //NOTE  why is this a bool and not a string
    internal bool Delete(int id)
    {
      string sql = @"
        DELETE FROM vaults
        WHERE id = @Id
        Limit 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;

    }
    internal IEnumerable<Vault> GetVaultsByAccount(string accountId)
    {
      string sql = @"
      SELECT v.*,
      a.*
      FROM vaults v
      JOIN accounts a
      ON v.creatorId = a.id
      WHERE v.creatorId = @accountId;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) => { v.Creator = a; return v; }, new { accountId }, splitOn: "id");

    }
    //// why do i have two of almost the exact same things 
    // internal List<Vault> GetMyVaults(Account userInfo)
    // {
    //   string sql =@"
    //  SELECT v.*,
    //   a.*
    //   FROM vaults v
    //   JOIN accounts a
    //   ON v.creatorId = a.id
    //   WHERE v.creatorId = @accountId;";
    //   return _db.Query<Account, Vault, Account>(sql, (a,v) =>{ v.Creator = a; return v;}, new {creatorId}).ToList();
    // }
    

  }
}