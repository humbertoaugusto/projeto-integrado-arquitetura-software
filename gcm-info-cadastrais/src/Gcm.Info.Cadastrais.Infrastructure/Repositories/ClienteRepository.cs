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
    /// Repositório de clientes
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public ClienteRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task Atualizar(Cliente cliente, CancellationToken ctx)
        {
            var sqlInsert =
                  $@"UPDATE Cliente SET
					nome = @Nome,
                    codigocampanha = @CodigoCampanha,
					aniversario = @Aniversario,
                    cep = @Cep,
                    logradouro = @Logradouro,
                    numero = @Numero,
                    complemento = @Complemento,
                    cidade = @Cidade,
                    estado = @Estado,
                    dataatualizacao = GETDATE()     
                  WHERE cpf = @Cpf";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Cpf", cliente.Cpf.Numero, System.Data.DbType.AnsiString);
            parameters.Add("@CodigoCampanha", cliente.CodigoCampanha, System.Data.DbType.Int32);            
            parameters.Add("@Nome", cliente.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Aniversario", cliente.Aniversario, System.Data.DbType.DateTime);
            parameters.Add("@Cep", cliente.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", cliente.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(cliente.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", cliente.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", cliente.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", cliente.Endereco.Estado, System.Data.DbType.AnsiString);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(string cpf, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM Cliente
				 WHERE cpf = @{nameof(cpf)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { cpf });
        }

        public async Task<List<Cliente>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					cpf,
					nome,
					aniversario,
                    codigocampanha,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao,
                    dataatualizacao
                FROM Cliente
                ORDER BY nome";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert);
            var listaClientes = new List<Cliente>();

            foreach (var item in listaDinamica.ToList())
                listaClientes.Add(ConverterSelectToCliente(item));

            return listaClientes;
        }

        public async Task<Cliente> ObterPorCpf(string cpf, CancellationToken ctx)
        {
            var sqlInsert =
               $@"SELECT 
                	id,
					cpf,
					nome,
                    codigocampanha,
					aniversario,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao,
                    dataatualizacao
                FROM Cliente
                WHERE cpf = @{nameof(cpf)}";

            using var connection = SqlServerDbContext.GetConnection();

            var clienteDinamico = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlInsert, new { cpf });
            return clienteDinamico != null ? ConverterSelectToCliente(clienteDinamico) : clienteDinamico;
        }

        public async Task Salvar(Cliente cliente, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Cliente
					(id,
					cpf,
					nome,
                    codigocampanha,
					aniversario,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao)
				VALUES 
					(@Id,
					@Cpf,
					@Nome,
                    @CodigoCampanha,
					@Aniversario,
                    @Cep,
                    @Logradouro,
                    @Numero,
                    @Complemento,
                    @Cidade,
                    @Estado,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", cliente.Id, System.Data.DbType.Guid);
            parameters.Add("@Cpf", cliente.Cpf.Numero, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", cliente.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@CodigoCampanha", cliente.CodigoCampanha, System.Data.DbType.Int32);
            parameters.Add("@Aniversario", cliente.Aniversario, System.Data.DbType.DateTime);
            parameters.Add("@Cep", cliente.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", cliente.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(cliente.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", cliente.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", cliente.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", cliente.Endereco.Estado, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", cliente.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(Cliente cliente, CancellationToken ctx)
        {
            var clienteExistente = await ObterPorCpf(cliente.Cpf.Numero, ctx);

            return clienteExistente?.Cpf.Numero == cliente.Cpf.Numero;
        }

        #region Métodos privados

        private Cliente ConverterSelectToCliente(dynamic select)
        {
            var endereco = new EnderecoCompleto(select.cep, select.logradouro, select.numero.ToString(), select?.complemento, select.cidade, select.estado);
            DateTime aniversario = select.aniversario;
            CPF cpf = new CPF(select.cpf);
            string nome = select.nome;
            int codigocampanha = 0;
            if (select.codigocampanha != null)                            
                codigocampanha = select.codigocampanha;

            return new Cliente(
                        nome,
                        cpf,                        
                        aniversario,
                        endereco,
                        codigocampanha
                    );
        }

        #endregion
    }
}