using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways.Interfaces;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de campanhas
    /// </summary>
    public class CampanhaRepository : ICampanhaRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }
        private IGcmInfoCadastraisGateway _gcmInfoCadastraisGateway { get; set; }

        public CampanhaRepository(
            ISqlServerDbContext sqlServerDbContext, 
            IGcmInfoCadastraisGateway gcmInfoCadastraisGateway)
        {
            SqlServerDbContext = sqlServerDbContext;
            _gcmInfoCadastraisGateway = gcmInfoCadastraisGateway;
        }

        public async Task Atualizar(CampanhaMkt campanha, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"UPDATE CampanhaMkt SET
					codigo = @Codigo,
					descricao = @Descricao,
					ativa = @Ativa,
					audienciaid = @AudienciaId,
                    dataatualizacao = GETDATE()     
                  WHERE codigo = @Codigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", campanha.Codigo, System.Data.DbType.Int32);
            parameters.Add("@Descricao", campanha.Descricao, System.Data.DbType.AnsiString);
            parameters.Add("@Ativa", campanha.Ativa, System.Data.DbType.Boolean);
            parameters.Add("@AudienciaId", campanha.AudienciaId, System.Data.DbType.Guid);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM CampanhaMkt
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        public async Task<List<CampanhaMkt>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,					
					descricao,
                    ativa,
                    audienciaid,
					datacriacao,
                    dataatualizacao
                FROM CampanhaMkt
                ORDER BY codigo";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<CampanhaMkt>(sqlInsert);
            return lista.ToList();
        }

        public async Task<CampanhaMkt> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					codigo,					
					descricao,
                    ativa,
                    audienciaid,
					datacriacao,
                    dataatualizacao
                FROM CampanhaMkt
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<CampanhaMkt>(sqlInsert, new { codigo });
        }

        public async Task Salvar(CampanhaMkt campanha, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO CampanhaMkt
					(id,
					codigo,					
					descricao,
                    ativa,
                    audienciaid,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@Descricao,
					@Ativa,
                    @AudienciaId,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", campanha.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", campanha.Codigo, System.Data.DbType.Int32);
            parameters.Add("@Descricao", campanha.Descricao, System.Data.DbType.AnsiString);
            parameters.Add("@Ativa", campanha.Ativa, System.Data.DbType.Boolean);
            parameters.Add("@AudienciaId", campanha.AudienciaId, System.Data.DbType.Guid);
            parameters.Add("@DataCriacao", campanha.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(CampanhaMkt campanha, CancellationToken ctx)
        {
            var campanhaExistente = await ObterPorCodigo(campanha.Codigo, ctx);

            return campanhaExistente?.Codigo == campanha.Codigo;
        }

        public async Task AtualizarCliente(CampanhaMkt campanha, CancellationToken ctx)
        {
            if (campanha.Ativa)
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
                WHERE id = @AudienciaId";

                using var connection = SqlServerDbContext.GetConnection();

                var audiencia = await connection.QueryFirstOrDefaultAsync<Audiencia>(sqlInsert, new { campanha.AudienciaId });
                foreach (var cpf in audiencia.Clientes.Split(","))
                {
                    var clienteGatewayModel = await _gcmInfoCadastraisGateway.ObterCliente(cpf, ctx);
                    if (clienteGatewayModel.Valid)
                    {
                        clienteGatewayModel.CodigoCampanha = campanha.Codigo;
                        await _gcmInfoCadastraisGateway.AtualizarCliente(clienteGatewayModel, ctx);
                    }
                }
            }
        }

        #region Métodos privados

        private CampanhaMkt ConverterSelectToCampanha(dynamic select)
        {
            int codigo = select.codigo;
            string descricao = select.descricao;
            bool ativa = select.ativa;
            Guid audienciaid = select.audienciaid;

            var campanha = new CampanhaMkt(
                            codigo,
                            descricao,
                            ativa,
                            audienciaid
                            );

            campanha.Id = select.id;
            return campanha;
        }

        #endregion
    }
}