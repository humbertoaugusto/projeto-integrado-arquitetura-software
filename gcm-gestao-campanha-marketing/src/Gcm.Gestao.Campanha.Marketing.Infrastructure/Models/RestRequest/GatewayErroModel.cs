using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Models.RestRequest
{
    public class GatewayErroModel : GatewayErroBase
    {
        public bool ErroNegocio { get; set; }
        public List<Error> Erros { get; set; }
        public override IReadOnlyCollection<Notification> ToNotifications()
        {
            return Erros.Select(e => e.ToNotification()).ToList();
        }
        public class Error
        {
            public string Mensagem { get; set; }
            public string Parametro { get; set; }
            public string CodigoErro { get; set; }

            public Notification ToNotification()
            {
                return new Notification(Parametro, Mensagem);
            }
        }
    }
}
