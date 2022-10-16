namespace Gcm.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados de endereço
    /// </summary>
    public class DadosEnderecoModel
    {
        /// <summary>
        /// CEP do endereço
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Descrição do logradouro (Rua, AV...)
        /// </summary>
        public string Logradouro { get; set; }
        /// <summary>
        /// Número do logradouro
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Complemento do logradouro
        /// </summary>
        public string Complemento { get; set; }
        /// <summary>
        /// Nome da Cidade
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Nome do Estado
        /// </summary>
        public string Estado { get; set; }
    }
}
