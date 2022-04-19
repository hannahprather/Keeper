using System.Data;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public int Create(VaultKeep vkData)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (vaultId, keepId, creatorId, id)
        VALUES
        (@VaultId, @KeepId, @CreatorId, @Id);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, vkData);
    }

    internal object GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
        SELECT k.*,
        vk.id as VaultKeepId,
        a.*
        FROM vaultkeeps vk
        JOIN keeps k
        ON k.id = vk.keepId
        JOIN accounts a 
        ON a.id = k.creatorId
        WHERE vaultId = @VaultId;";
      return _db.Query<VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (keep, account) => { keep.Creator = account; return keep; }, new { vaultId }, splitOn: "id");
    }

    internal VaultKeep GetKeepById(int id)
    {
      string sql = @"
      SELECT * 
      FROM vaultkeeps
      WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
    internal bool DeleteVaultKeep(int id)
    {
      string sql = @"DELETE FROM vaultkeeps
       WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

  }
}