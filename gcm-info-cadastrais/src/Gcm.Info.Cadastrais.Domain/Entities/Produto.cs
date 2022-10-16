using Flunt.Validations;
using System;
using Gcm.Info.Cadastrais.Domain.Entities.Core;

namespace Gcm.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de produto
    /// </summary>
    public class Produto : Entity
    {
        /// <summary>
        /// Construtor padrão de Produto
        /// </summary>
        public Produto() { }

        /// <summary>
        /// Construtor de produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nome"></param>
        /// <param name="valor"></param>     
        /// <param name="quantidade"></param>
        /// <param name="categoriaProdutoId"></param>
        public Produto(int codigo, string nome, double valor, int quantidade, Guid categoriaProdutoId)
        {
            Codigo = codigo;
            Nome = nome;
            Valor = valor;
            Quantidade = quantidade;
            CategoriaProdutoId = categoriaProdutoId;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo"));
        }

        /// <summary>
        /// Codigo de identificação do produto
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Valor do produto
        /// </summary>
        public double Valor { get; set; }
        /// <summary>
        /// quantidade de produto
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Id do categoriaProduto
        /// </summary>
        public Guid CategoriaProdutoId { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
