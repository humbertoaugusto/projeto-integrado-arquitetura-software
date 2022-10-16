using Flunt.Notifications;
using System;

namespace Gcm.Info.Cadastrais.Domain.Entities.Core
{
    public abstract class Entity : Notifiable
    {
        private Guid _id;
        public virtual Guid Id
        {
            get => _id;
            set => _id = value == Guid.Empty ? Guid.NewGuid() : value;
        }

        protected Entity() => Id = Guid.NewGuid();
    }
}
