using Flunt.Notifications;
using System.Diagnostics.CodeAnalysis;

namespace Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class ValueObject : Notifiable
    {
        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }
    }
    [ExcludeFromCodeCoverage]
    public abstract class ValueObject<T> : Notifiable
    {
        protected ValueObject(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public ValueObject<T> GetCopy()
        {
            return MemberwiseClone() as ValueObject<T>;
        }
    }
    [ExcludeFromCodeCoverage]
    public abstract class ValueObjectString : ValueObject<string>
    {
        protected ValueObjectString(string value) : base(value?.Trim()) { }
        public override string ToString() => Value;
        public static implicit operator string(ValueObjectString valueObject) => valueObject?.Value;
    }
}
