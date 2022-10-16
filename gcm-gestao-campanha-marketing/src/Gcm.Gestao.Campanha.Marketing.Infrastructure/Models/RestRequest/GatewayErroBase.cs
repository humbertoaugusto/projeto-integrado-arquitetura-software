using Flunt.Notifications;
using System.Collections.Generic;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Models.RestRequest
{
    /// <summary>
    /// Classe base de erros de gateway
    /// </summary>
    public abstract class GatewayErroBase
    {
        public abstract IReadOnlyCollection<Notification> ToNotifications();
    }
}
