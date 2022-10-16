using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways.Interfaces
{
    public interface IGcmInfoCadastraisGateway
    {
        Task AtualizarCliente(ClienteGatewayModel clienteGatewayModel, CancellationToken ctx);
        Task<ClienteGatewayModel> ObterCliente(string cpf, CancellationToken ctx);
    }
}