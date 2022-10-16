using Flunt.Validations;
using Gcm.Info.Cadastrais.Domain.ValueObjects;
using System;
using Gcm.Info.Cadastrais.Domain.Entities.Core;

namespace Gcm.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de categoriaProduto
    /// </summary>
    public class CategoriaProduto : Entity
    {
        /// <summary>
        /// Construtor padrão de categoriaProduto
        /// </summary>
        public CategoriaProduto() { }

        /// <summary>
        /// Construtor de categoriaProduto
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="descricao"></param>        
        /// <param name="endereco"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public CategoriaProduto(int codigo, string nome, string descricao)
        {
            Codigo = Codigo;
            Nome = nome;
            Descricao = descricao;            
            DataCriacao = DateTime.UtcNow;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo"));
        }
        /// <summary>
        /// Codigo de identificação da categoriaProduto
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome do categoriaProduto
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descricao da categoriaProduto
        /// </summary>
        public string Descricao { get; set; }        
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
