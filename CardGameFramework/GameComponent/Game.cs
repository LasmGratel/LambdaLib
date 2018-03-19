using System;
using System.Collections.Generic;
using System.Text;
using CardGameFramework.Rules;

namespace CardGameFramework.GameComponent
{
    public class Game : IIdentifiable<string>
    {
        public Game(Identifier<string> identifier)
        {
            Identifier = identifier;
        }

        public ICollection<Rule> Rules { get; }
        public Identifier<string> Identifier { get; }
    }
}
