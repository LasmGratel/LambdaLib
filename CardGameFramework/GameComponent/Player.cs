using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public class Player : IIdentifiable<string>
    {
        public Player(Identifier<string> identifier)
        {
            Identifier = identifier;
        }

        public Identifier<string> Identifier { get; }
    }
}