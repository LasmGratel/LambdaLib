using System;
using System.Collections.Generic;
using System.Text;
using CardGameFramework.Rules;

namespace CardGameFramework.GameComponent
{
    class Game
    {
        public ICollection<Rule> Rules { get; }
    }
}
