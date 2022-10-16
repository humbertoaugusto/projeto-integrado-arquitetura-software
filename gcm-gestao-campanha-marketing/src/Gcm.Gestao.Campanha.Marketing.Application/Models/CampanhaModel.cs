using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Gcm.Gestao.Campanha.Marketing.Application.Models
{
    /// <summary>
    /// Modelo de dados de pedido
    /// </summary>
    public class CampanhaModel
    {
        /// <summary>
        /// Id da Campanha
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Codigo
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Descricao
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Ativa
        /// </summary>
        public bool Ativa { get; set; }
        /// <summary>
        /// Id do CategoriaProduto
        /// </summary>
        public Guid AudienciaId { get; set; }
    }
}
