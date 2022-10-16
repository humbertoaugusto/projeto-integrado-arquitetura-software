using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Gcm.Gestao.Campanha.Marketing.Application.Models
{
    /// <summary>
    /// Modelo de dados de estoque
    /// </summary>
    public class AudienciaModel
    {
        /// <summary>
        /// Id da Audiencia
        /// </summary>
        public Guid Id { get; set; }
        /// Codigo do estoque
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// audiencia
        /// </summary>
        public string FiltroAudiencia { get; set; }
        /// <summary>
        /// clientes
        /// </summary>
        public string Clientes { get; set; }
    }
}
