using Gcm.Info.Cadastrais.Domain.Entities;
using Gcm.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Gcm.Info.Cadastrais.Domain.ValueObjects;

namespace Gcm.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de categoriaProdutoes
    /// </summary>
    public class CategoriaProdutoRepository : ICategoriaProdutoRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public CategoriaProdutoRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task Atualizar(CategoriaProduto categoriaProduto, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"UPDATE CategoriaProduto SET
					nome = @Nome,
					descricao = @Descricao,
                    dataatualizacao = GETDATE()     
                  WHERE codigo = @Codigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", categoriaProduto.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", categoriaProduto.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Descricao", categoriaProduto.Descricao, System.Data.DbType.AnsiString);            

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM CategoriaProduto
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        public async Task<List<CategoriaProduto>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					nome,
					descricao,
					datacriacao,
                    dataatualizacao
                FROM CategoriaProduto
                ORDER BY nome";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<CategoriaProduto>(sqlInsert);
            return lista.ToList();
        }

        public async Task<CategoriaProduto> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					codigo,
					nome,
					descricao,
					datacriacao,
                    dataatualizacao
                FROM CategoriaProduto
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<CategoriaProduto>(sqlInsert, new { codigo });
        }

        public async Task Salvar(CategoriaProduto categoriaProduto, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO CategoriaProduto
					(id,
					codigo,
					nome,
					descricao,					
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@Nome,
					@Descricao,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", categoriaProduto.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", categoriaProduto.Codigo, System.Data.DbType.Int32);
            parameters.Add("@Nome", categoriaProduto.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Descricao", categoriaProduto.Descricao, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", categoriaProduto.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(CategoriaProduto categoriaProduto, CancellationToken ctx)
        {
            var categoriaProdutoExistente = await ObterPorCodigo(categoriaProduto.Codigo, ctx);

            return categoriaProdutoExistente?.Codigo == categoriaProduto.Codigo;
        }

        #region Métodos privados

        private CategoriaProduto ConverterSelectToCategoriaProduto(dynamic select)
        {            
            int codigo = select.codigo;
            string nome = select.nome;
            string descricao = select.descricao;

            var categoriaProduto = new CategoriaProduto(
                                codigo,
                                nome,
                                descricao
                            );

            categoriaProduto.Id = select.id;
            return categoriaProduto;
        }

        #endregion
    }
}