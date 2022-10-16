using Flunt.Validations;
using Gcm.Info.Cadastrais.Domain.ValueObjects.Core;

namespace Gcm.Info.Cadastrais.Domain.ValueObjects
{
    /// <summary>
    /// Objeto de CPF
    /// </summary>
    public class CPF : ValueObject
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="numero"></param>
        public CPF(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(CPF), "CPF não pode ser nulo ou branco")
                .HasLen(Numero, 11, nameof(CPF), "CPF deve conter 11 posições")
                .IsDigit(Numero, nameof(CPF), "CPF deve conter apenas números")
                .IsTrue(IsCpf(Numero), nameof(CPF), "CPF informado não é válido"));
        }

        /// <summary>
        /// Número do CPF
        /// </summary>
        public string Numero { get; private set; }

        /// <summary>
        /// Converter em string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Numero;
        }

        /// <summary>
        /// Verifica se o CPF é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private bool IsCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            char[] datachars = cpf.ToCharArray();
            foreach (var datachar in datachars)            
                if (!char.IsDigit(datachar))
                    return false;  

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;            

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}