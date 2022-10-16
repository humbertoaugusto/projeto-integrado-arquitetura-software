using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;

namespace Gcm.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de CategoriaProdutoApplication
    /// </summary>
    public interface ICategoriaProdutoApplication
    {
        /// <summary>
        /// Obtém a lista de todos os categoriaProdutoes
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<CategoriaProdutoModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de uma categoriaProduto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CategoriaProdutoModel>> ObterCategoriaProduto(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de uma categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CategoriaProduto>> CadastrarCategoriaProduto(CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de uma categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CategoriaProduto>> AtualizarCategoriaProduto (CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx);

        /// <summary>
        /// Deletar uma categoriaProduto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CategoriaProduto>> DeletarCategoriaProduto(int codigo, CancellationToken ctx);
    }
}