using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public struct Identifier<T> : IEquatable<Identifier<T>>
    {
        public T Value { get; }

        public Identifier(T value)
        {
            Value = value == null ? throw new ArgumentNullException("Identifier cannot be null!") : value;
        }

        public bool Equals(Identifier<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Identifier<T> && Equals((Identifier<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Identifier<T> left, T right) => Equals(left.Value, right);
        public static bool operator !=(Identifier<T> left, T right) => !Equals(left.Value, right);

        public static Identifier<T> Of(T value)
        {
            return new Identifier<T>(value);
        }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }

    public interface IIdentifiable<T>
    {
        Identifier<T> Identifier { get; }
    }
}
