using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public class CardFactory
    {
        public static ICardFactory Get(string name)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICardFactory
    {
        ICollection<Card> Deck { get; }

        Card GenerateFromAmount(int amount);

        IEnumerable<Card> GenerateFromCount(int count);
    }

    public static class CardFactoryExtensions
    {
        public static IEnumerable<Card> GenerateFromAmount(this ICardFactory factory, int amount, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return factory.GenerateFromAmount(amount);
            }
        }
    }
}