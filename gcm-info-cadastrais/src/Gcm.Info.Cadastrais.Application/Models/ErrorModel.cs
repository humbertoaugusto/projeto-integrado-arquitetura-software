using Flunt.Notifications;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gcm.Info.Cadastrais.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorModel
    {
        public List<string> Erros { get; } = new List<string>();

        public ErrorModel(string erro)
        {
            Erros.Add(erro);
        }

        public ErrorModel(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }

        public ErrorModel(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                Erros.Add(notification.Message);
            }
        }
    }
}
