using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public class Card : IEquatable<Card>
    {
        public Color DrawColor => Color.ToDrawColor();
        public CardColor Color { get; }
        public CardAmount Amount { get; }
        public CardType Type { get; }

        public bool Equals(Card other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Color.Equals(other.Color) && Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Color.GetHashCode() * 397) ^ Amount.GetHashCode();
            }
        }

        public static bool operator ==(Card left, Card right) => Equals(left, right);

        public static bool operator !=(Card left, Card right) => !Equals(left, right);
    }

    public class CardType : IEquatable<CardType>
    {
        public string Type { get; }

        public CardType(string type) => Type = type;

        public bool Equals(CardType other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || string.Equals(Type, other.Type);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((CardType)obj);
        }

        public override int GetHashCode() => Type.GetHashCode();

        public static bool operator ==(CardType left, CardType right) => Equals(left, right);

        public static bool operator !=(CardType left, CardType right) => !Equals(left, right);

        public static implicit operator string(CardType type) => type.Type;

        public static implicit operator CardType(string type) => new CardType(type);
    }

    public class CardAmount : IComparable<CardAmount>, IComparable
    {
        public int Amount { get; }

        protected bool Equals(CardAmount other) => Amount == other.Amount;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((CardAmount)obj);
        }

        public int CompareTo(CardAmount other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return other is null ? 1 : Amount.CompareTo(other.Amount);
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is CardAmount)) throw new ArgumentException($"Object must be of type {nameof(CardAmount)}");
            return CompareTo((CardAmount)obj);
        }

        public static bool operator <(CardAmount left, CardAmount right) => Comparer<CardAmount>.Default.Compare(left, right) < 0;

        public static bool operator >(CardAmount left, CardAmount right) => Comparer<CardAmount>.Default.Compare(left, right) > 0;

        public static bool operator <=(CardAmount left, CardAmount right) => Comparer<CardAmount>.Default.Compare(left, right) <= 0;

        public static bool operator >=(CardAmount left, CardAmount right) => Comparer<CardAmount>.Default.Compare(left, right) >= 0;

        public override int GetHashCode() => Amount;

        public static bool operator ==(CardAmount left, CardAmount right) => Equals(left, right);

        public static bool operator !=(CardAmount left, CardAmount right) => !Equals(left, right);

        public static implicit operator int(CardAmount amount) => amount.Amount;

        public CardAmount(int amount) => Amount = amount;

        public static implicit operator CardAmount(int amount) => new CardAmount(amount);
    }

    public class CardColor : IEquatable<CardColor>
    {
        public int? Index { get; set; }

        public CardColor(int index) => Index = index;

        public static CardColor Of(int index) => new CardColor(index);

        public virtual Color ToDrawColor()
        {
            return Color.Aqua;
        }

        public bool Equals(CardColor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((CardColor)obj);
        }

        public override int GetHashCode() => Index ?? 0;

        public static bool operator ==(CardColor left, CardColor right) => Equals(left, right);

        public static bool operator !=(CardColor left, CardColor right) => !Equals(left, right);
    }

    public class CardGroup
    {
        public CardGroup(int amount, int count, CardType type)
        {
            Amount = amount;
            Count = count;
            Type = type;
        }

        public int Amount { get; }
        public int Count { get; }
        public CardType Type { get; }
    }

    public static class CardExtensions
    {
        public static unsafe List<CardGroup> ToCardGroups(this ICollection<Card> cards)
        {
            var enumerable = cards;
            var cardnums = enumerable.Select(card => (int)card.Amount);
            var last = enumerable.Last();
            var length = last.Amount + 1;
            var array = stackalloc int[length];
            SetAllZero(array, length);
            foreach (var num in cardnums) array[num]++;
            var o = new List<CardGroup>(length);
            for (var i = 0; i < length; i++)
            {
                var num = array[i];
                if (num != 0) o.Add(new CardGroup(i, num, last.Type));
            }

            return o;

            void SetAllZero(int* source, int len)
            {
                for (var i = 0; i < len; i++) source[i] = 0;
            }
        }

        public static IEnumerable<Card> ToCards(this CardGroup cardGroup)
        {
            var factory = CardFactory.Get(cardGroup.Type);
            return ToCards(cardGroup, factory);
        }

        public static IEnumerable<Card> ToCards(this CardGroup cardGroup, ICardFactory cardFactory)
        {
            return cardFactory.GenerateFromAmount(cardGroup.Amount, cardGroup.Count);
        }

        public static IEnumerable<Card> ToCards(this ICollection<CardGroup> cardGroups)
        {
            var factory = CardFactory.Get(cardGroups.First().Type);
            return cardGroups.SelectMany(group => @group.ToCards(factory));
        }
    }
}