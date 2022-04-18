using System.Data;
using System.Linq;
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
        vaultkeep.id as VaultKeepId,
        a.*
        FROM vaultkeeps vaultkeep
        JOIN keeps k
        ON k.id = vaultkeep.keepId
        JOIN accounts a 
        ON a.id = k.creatorId
        WHERE vaultId = @VaultId;";
      return _db.Query<Keep, Account, Keep>(sql, (valk, prof) =>
      {
        valk.Creator = prof;
        return valk;
      }, new { vaultId }).FirstOrDefault();
    }
  }
}