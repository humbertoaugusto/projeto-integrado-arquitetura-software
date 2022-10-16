using Gcm.Info.Cadastrais.Domain.Entities;
using Gcm.Info.Cadastrais.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Gcm.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de produtos
    /// </summary>
    public class ProdutoRepository : IProdutoRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public ProdutoRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }
        public async Task Salvar(Produto produto, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Produto
					(id,
					codigo,
					nome,
					quantidade,
					valor,
                    categoriaprodutoid,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@Nome,
					@Quantidade,
					@Valor,
                    @CategoriaProdutoId,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", produto.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", produto.Codigo, System.Data.DbType.Int32);
            parameters.Add("@Nome", produto.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Quantidade", produto.Quantidade, System.Data.DbType.Int32);
            parameters.Add("@Valor", produto.Valor, System.Data.DbType.Decimal);
            parameters.Add("@CategoriaProdutoId", produto.CategoriaProdutoId, System.Data.DbType.Guid);
            parameters.Add("@DataCriacao", produto.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<Produto> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					nome,
					quantidade,
					valor,
                    categoriaProdutoId,
					datacriacao,
                    dataatualizacao
                FROM Produto
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Produto>(sqlInsert, new { codigo });
        }

        public async Task<bool> VerificarSeExiste(Produto produto, CancellationToken ctx)
        {
            var produtoExistente = await ObterPorCodigo(produto.Codigo, ctx);

            return produtoExistente?.Codigo == produto.Codigo;
        }

        public async Task<List<Produto>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					nome,
					quantidade,
					valor,
                    categoriaProdutoId,
					datacriacao,
                    dataatualizacao
                FROM Produto
                ORDER BY codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<Produto>(sqlInsert);
            return lista.ToList();
        }

        public async Task Atualizar(Produto produto, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE Produto SET
                  nome = @{ nameof(produto.Nome)},
				  quantidade = @{ nameof(produto.Quantidade)},
                  valor = @{ nameof(produto.Valor)},
                  dataatualizacao = GETDATE()     
                WHERE codigo = @{nameof(produto.Codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, produto);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
               $@"DELETE FROM Produto
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }
    }    
}