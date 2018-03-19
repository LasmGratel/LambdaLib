using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public class Desk : IIdentifiable<string>
    {
        public Desk(Identifier<string> identifier)
        {
            Identifier = identifier;
        }

        public Identifier<string> Identifier { get; }
    }
}
