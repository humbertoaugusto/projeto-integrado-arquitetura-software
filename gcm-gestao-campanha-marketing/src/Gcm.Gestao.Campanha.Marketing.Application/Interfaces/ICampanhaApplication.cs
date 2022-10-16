using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;

namespace Gcm.Gestao.Campanha.Marketing.Application.Interfaces
{
    /// <summary>
    /// Interface de Campanha Application
    /// </summary>
    public interface ICampanhaApplication
    {
        /// <summary>
        /// ListarTodos
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<CampanhaModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// ObterCampanha
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CampanhaModel>> ObterCampanha(int codigo, CancellationToken ctx);

        /// <summary>
        /// CadastrarCampanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CampanhaMkt>> CadastrarCampanha(CampanhaModel campanhaModel, CancellationToken ctx);

        /// <summary>
        /// AtualizarCampanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CampanhaMkt>> AtualizarCampanha(CampanhaModel campanhaModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um campanha
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<CampanhaMkt>> DeletarCampanha(int codigo, CancellationToken ctx);
    }
}