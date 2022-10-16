using Gcm.Info.Cadastrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de clientes
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>
        /// Armazena um cliente no banco de dados
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Cliente cliente, CancellationToken ctx);
        /// <summary>
        /// Obtém o cliente por cpf
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Cliente> ObterPorCpf(string cpf, CancellationToken ctx);
        /// <summary>
        /// Verifica se o cliente já existe no banco
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Cliente cliente, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de clientes
        /// </summary>
        /// <returns></returns>
        Task<List<Cliente>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um cliente
        /// </summary>
        /// <returns></returns>
        /// <param name="cliente"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Cliente cliente, CancellationToken ctx);
        /// <summary>
        /// Deleta o cliente pelo cpf
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(string cpf, CancellationToken ctx);
    }
}
