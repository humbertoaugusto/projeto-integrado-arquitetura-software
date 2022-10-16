using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de campanhas
    /// </summary>
    public interface ICampanhaRepository
    {
        Task Atualizar(CampanhaMkt campanhaMkt, CancellationToken ctx);
        Task Deletar(int codigo, CancellationToken ctx);
        Task<List<CampanhaMkt>> ListarTodos(CancellationToken ctx);
        Task<CampanhaMkt> ObterPorCodigo(int codigo, CancellationToken ctx);
        Task Salvar(CampanhaMkt campanhaMkt, CancellationToken ctx);
        Task<bool> VerificarSeExiste(CampanhaMkt campanhaMkt, CancellationToken ctx);
        Task AtualizarCliente(CampanhaMkt campanha, CancellationToken ctx);
    }
}
