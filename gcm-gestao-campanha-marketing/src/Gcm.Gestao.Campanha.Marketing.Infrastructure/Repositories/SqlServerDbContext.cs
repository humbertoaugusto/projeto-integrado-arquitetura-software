using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Repositories
{
    /// <summary>
    /// Classe de contexto da conexao sql
    /// </summary>
    public class SqlServerDbContext : ISqlServerDbContext
    {
        /// <summary>
        /// Connection String
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public SqlServerDbContext()
        {
            connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
        }

        /// <summary>
        /// Obtém a conexão com o banco
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(connectionString);
            Dapper.SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
            conn.Open();
            Console.WriteLine("State: {0}", conn.State);
            Console.WriteLine("ConnectionTimeout: {0}",
                conn.ConnectionTimeout);
            return conn;
        }
    }
}
