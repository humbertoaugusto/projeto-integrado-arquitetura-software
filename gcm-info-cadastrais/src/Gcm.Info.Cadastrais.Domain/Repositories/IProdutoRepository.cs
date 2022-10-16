using Gcm.Info.Cadastrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de produtos
    /// </summary>
    public interface IProdutoRepository
    {
        /// <summary>
        /// Armazena um produto no banco de dados
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Produto produto, CancellationToken ctx);
        /// <summary>
        /// Obtém o produto por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Produto> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o produto já existe no banco
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Produto produto, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de produto
        /// </summary>
        /// <returns></returns>
        Task<List<Produto>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um produto
        /// </summary>
        /// <returns></returns>
        /// <param name="produto"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Produto produto, CancellationToken ctx);
        /// <summary>
        /// Deleta o produto por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(int codigo, CancellationToken ctx);
    }
}
