using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;

namespace Gcm.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de ProdutoApplication 
    /// </summary>
    public interface IProdutoApplication
    {
        /// <summary>
        /// Obtém a lista de todos os produtos
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<ProdutoModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<ProdutoModel>> ObterProduto(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um Produto
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Produto>> CadastrarProduto(ProdutoModel produtoModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de um Produto
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Produto>> AtualizarProduto(ProdutoModel produtoModel, CancellationToken ctx);

        /// <summary>
        /// Deletar de um produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Produto>> DeletarProduto(int codigo, CancellationToken ctx);
    }
}