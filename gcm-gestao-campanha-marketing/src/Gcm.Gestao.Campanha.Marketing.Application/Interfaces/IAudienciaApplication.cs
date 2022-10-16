using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;

namespace Gcm.Gestao.Campanha.Marketing.Application.Interfaces
{
    /// <summary>
    /// Interface de Audiencia Application
    /// </summary>
    public interface IAudienciaApplication
    {
        /// <summary>
        /// Obtém a lista de todos os audiencias
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<AudienciaModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de uma audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<AudienciaModel>> ObterAudiencia(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de uma audiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Audiencia>> CadastrarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx);

        /// <summary>
        /// AtualizarAudiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Audiencia>> AtualizarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Audiencia>> DeletarAudiencia(int codigo, CancellationToken ctx);
    }
}