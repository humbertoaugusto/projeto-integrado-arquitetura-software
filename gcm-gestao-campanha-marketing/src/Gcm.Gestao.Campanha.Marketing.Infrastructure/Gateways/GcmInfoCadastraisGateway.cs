using Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Helpers;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models.RestRequest;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways
{
    public class GcmInfoCadastraisGateway : IGcmInfoCadastraisGateway
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="httpClient"></param>
        public GcmInfoCadastraisGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Atualiza as informações de um cliente
        /// </summary>
        /// <param name="clienteGatewayModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task AtualizarCliente(ClienteGatewayModel clienteGatewayModel, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/clientes";
            
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = new StringContent(JsonSerializer.Serialize(clienteGatewayModel), Encoding.UTF8, "application/json");

            await _httpClient.SendAsync(request, ctx);
        }

        /// <summary>
        /// Obtem os dados de um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<ClienteGatewayModel> ObterCliente(string cpf, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/clientes/{cpf}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<ClienteGatewayModel, GatewayErroModel>(result);
        }
    }
}
