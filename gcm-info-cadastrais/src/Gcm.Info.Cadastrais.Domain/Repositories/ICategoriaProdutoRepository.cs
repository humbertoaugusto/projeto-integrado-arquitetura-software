using Gcm.Info.Cadastrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de categoriaProdutoes
    /// </summary>
    public interface ICategoriaProdutoRepository
    {
        /// <summary>
        /// Armazena uma categoriaProduto no banco de dados
        /// </summary>
        /// <param name="categoriaProduto"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(CategoriaProduto categoriaProduto, CancellationToken ctx);
        /// <summary>
        /// Obtém o categoriaProduto por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<CategoriaProduto> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o categoriaProduto já existe no banco
        /// </summary>
        /// <param name="categoriaProduto"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(CategoriaProduto categoriaProduto, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de categoriaProdutoes
        /// </summary>
        /// <returns></returns>
        Task<List<CategoriaProduto>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza uma categoriaProduto
        /// </summary>
        /// <returns></returns>
        /// <param name="categoriaProduto"></param>
        /// <param name="ctx"></param>
        Task Atualizar(CategoriaProduto categoriaProduto, CancellationToken ctx);
        /// <summary>
        /// Deleta o categoriaProduto pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(int codigo, CancellationToken ctx);
    }
}
