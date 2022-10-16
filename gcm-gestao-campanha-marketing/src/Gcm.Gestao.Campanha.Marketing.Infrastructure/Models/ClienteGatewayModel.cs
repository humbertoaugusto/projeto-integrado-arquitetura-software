using Flunt.Notifications;
using System;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Models
{
    /// <summary>
    /// Modelo de dados de cliente
    /// </summary>
    public class ClienteGatewayModel : Notifiable
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cpf do cliente
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Codigo referente a Campanha de marketing no qual o cliente foi atribuido
        /// </summary>
        public int CodigoCampanha { get; set; }
        /// <summary>
        /// Data de aniversário do cliente
        /// </summary>
        public DateTime Aniversario { get; set; }
        /// <summary>
        /// Dados do endereço do cliente
        /// </summary>
        public DadosEnderecoModel Endereco { get; set; }
    }
}
