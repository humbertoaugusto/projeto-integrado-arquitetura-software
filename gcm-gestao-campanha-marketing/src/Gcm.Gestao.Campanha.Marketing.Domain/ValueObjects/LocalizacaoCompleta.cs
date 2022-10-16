using Flunt.Validations;
using Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects.Core;

namespace Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects
{
    /// <summary>
    /// Localização completa
    /// </summary>
    public class LocalizacaoCompleta : ValueObject
    {       
        /// <summary>
        /// Construtor da classe 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public LocalizacaoCompleta(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Latitude, nameof(Latitude), "Latitude não pode ser nulo ou vazia")
                .IsNotNullOrWhiteSpace(Longitude, nameof(Longitude), "Longitude não pode ser nula ou vazia"));
        }

        /// <summary>
        /// Latitude da localização
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude da localização
        /// </summary>
        public string Longitude { get; set; }       
    }
}