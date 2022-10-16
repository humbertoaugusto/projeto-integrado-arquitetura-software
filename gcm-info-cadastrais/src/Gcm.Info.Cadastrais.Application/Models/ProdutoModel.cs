using System;

namespace Gcm.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados do produto
    /// </summary>
    public class ProdutoModel
    {
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
        /// Quantidade de produto
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Id do CategoriaProduto
        /// </summary>
        public Guid CategoriaProdutoId { get; set; }
    }
}
