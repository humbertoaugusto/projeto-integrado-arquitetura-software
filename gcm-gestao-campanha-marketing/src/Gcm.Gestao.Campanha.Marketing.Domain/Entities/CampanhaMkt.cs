using System;
using System.Collections.Generic;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities.Core;

namespace Gcm.Gestao.Campanha.Marketing.Domain.Entities
{
    /// <summary>
    /// Classe da entidade Campanha
    /// </summary>
    public class CampanhaMkt : Entity
    {
        /// <summary>
        /// Construtor padrão Campanha
        /// </summary>
        public CampanhaMkt() { }

        /// <summary>
        /// Construtor Campanha
        /// </summary>     
        public CampanhaMkt(int codigo, string descricao, bool ativa,  Guid audienciaId)
        {
            Codigo = codigo;
            Descricao = descricao;
            Ativa = ativa;
            AudienciaId = audienciaId;
            DataCriacao = DateTime.UtcNow;      
        }

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
        /// Id da Audiencia
        /// </summary>
        public Guid AudienciaId { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
