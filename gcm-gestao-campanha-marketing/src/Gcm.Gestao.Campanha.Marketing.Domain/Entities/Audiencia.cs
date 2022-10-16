using System;
using System.Collections.Generic;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities.Core;

namespace Gcm.Gestao.Campanha.Marketing.Domain.Entities
{
    /// <summary>
    /// Classe da entidade Audiencia
    /// </summary>
    public class Audiencia : Entity
    {
        /// <summary>
        /// Construtor padrão Audiencia
        /// </summary>
        public Audiencia() { }

        /// <summary>
        /// Construtor Audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="filtroAudiencia"></param>
        /// <param name="clientes"></param>        
        public Audiencia(int codigo, string filtroAudiencia, string clientes)
        {
            Codigo = codigo;
            FiltroAudiencia = filtroAudiencia;
            Clientes = clientes;            
            DataCriacao = DateTime.UtcNow;
        }

        /// Codigo 
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// FiltroAudiencia
        /// </summary>
        public string FiltroAudiencia { get; set; }
        /// <summary>
        /// clientes
        /// </summary>
        public string Clientes { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
