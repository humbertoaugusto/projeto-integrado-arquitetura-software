using System.Data;

namespace Gcm.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Conexão com SqlServer
    /// </summary>
    public interface ISqlServerDbContext
    {
        /// <summary>
        /// GetConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
