using System;

namespace Gcm.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados de categoriaProduto
    /// </summary>
    public class CategoriaProdutoModel
    {
        /// <summary>
        /// Id da categoriaProduto
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Codigo de identificação da categoriaProduto
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome da categoriaProduto
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descricao da categoriaProduto
        /// </summary>
        public string Descricao { get; set; }
    }
}
