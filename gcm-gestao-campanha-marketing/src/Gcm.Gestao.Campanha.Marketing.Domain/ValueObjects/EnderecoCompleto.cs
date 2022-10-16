using Flunt.Validations;
using Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects.Core;

namespace Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects
{
    /// <summary>
    /// Endereço completo 
    /// </summary>
    public class EnderecoCompleto : ValueObject
    {       
        /// <summary>
        /// Construtor da classe 
        /// </summary>
        /// <param name="cep"></param>
        /// <param name="logradouro"></param>
        /// <param name="numero"></param>
        /// <param name="complemento"></param>
        /// <param name="cidade"></param>
        /// <param name="estado"></param>
        public EnderecoCompleto(string cep, string logradouro, string numero, string complemento, string cidade, string estado)
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Cep, nameof(Cep), "Cep não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Logradouro, nameof(Logradouro), "Logradouro não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Numero, nameof(Numero), "Numero não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Cidade, nameof(Cidade), "Cidade não pode ser nula ou branca")
                .IsNotNullOrWhiteSpace(Estado, nameof(Estado), "Estado não pode ser nulo ou branco"));
        }

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

        /// <summary>
        /// Conversão do endereço completo para string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", Logradouro, Numero, Cidade, Estado, Cep);
        }
    }
}