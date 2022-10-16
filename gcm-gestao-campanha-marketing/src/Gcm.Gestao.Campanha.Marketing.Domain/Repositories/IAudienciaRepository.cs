using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de pedido
    /// </summary>
    public interface IAudienciaRepository
    {
        Task Atualizar(Audiencia audiencia, CancellationToken ctx);
        Task Deletar(int codigo, CancellationToken ctx);
        Task<List<Audiencia>> ListarTodos(CancellationToken ctx);
        Task<Audiencia> ObterPorCodigo(int codigo, CancellationToken ctx);
        Task Salvar(Audiencia audiencia, CancellationToken ctx);
        Task<bool> VerificarSeExiste(Audiencia audiencia, CancellationToken ctx);
    }
}
