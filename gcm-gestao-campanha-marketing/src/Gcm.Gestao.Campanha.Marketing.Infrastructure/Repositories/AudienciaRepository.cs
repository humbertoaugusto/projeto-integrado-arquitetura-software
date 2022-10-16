using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de audiencias
    /// </summary>
    public class AudienciaRepository : IAudienciaRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public AudienciaRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task Atualizar(Audiencia audiencia, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"UPDATE Audiencia SET
					codigo = @Codigo,
					filtroaudiencia = @FiltroAudiencia,
                    clientes = @Clientes,
                    dataatualizacao = GETDATE()     
                  WHERE codigo = @Codigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", audiencia.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@FiltroAudiencia", audiencia.FiltroAudiencia, System.Data.DbType.AnsiString);
            parameters.Add("@Clientes", audiencia.Clientes, System.Data.DbType.AnsiString);            

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM Audiencia
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        public async Task<List<Audiencia>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					filtroaudiencia,
					clientes,
					datacriacao,
                    dataatualizacao
                FROM Audiencia
                ORDER BY codigo";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<Audiencia>(sqlInsert);
            return lista.ToList();
        }

        public async Task<Audiencia> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					codigo,
					filtroaudiencia,
					clientes,
					datacriacao,
                    dataatualizacao
                FROM Audiencia
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Audiencia>(sqlInsert, new { codigo });
        }

        public async Task Salvar(Audiencia audiencia, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Audiencia
					(id,
					codigo,
					filtroaudiencia,
					clientes,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@FiltroAudiencia,
					@Clientes,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", audiencia.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", audiencia.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@FiltroAudiencia", audiencia.FiltroAudiencia, System.Data.DbType.AnsiString);
            parameters.Add("@Clientes", audiencia.Clientes, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", audiencia.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(Audiencia audiencia, CancellationToken ctx)
        {
            var audienciaExistente = await ObterPorCodigo(audiencia.Codigo, ctx);

            return audienciaExistente?.Codigo == audiencia.Codigo;
        }

        #region Métodos privados

        private Audiencia ConverterSelectToAudiencia(dynamic select)
        {
            int codigo = select.codigo;
            string filtroaudiencia = select.filtroaudiencia;
            string clientes = select.clientes;

            var audiencia = new Audiencia(
                                codigo,
                                filtroaudiencia,
                                clientes
                            );

            audiencia.Id = select.id;
            return audiencia;
        }

        #endregion
    }
}