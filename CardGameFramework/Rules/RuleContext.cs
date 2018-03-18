using System;
using System.Collections.Generic;
using System.Text;
using CardGameFramework.GameComponent;

namespace CardGameFramework.Rules
{
    public class RuleContext
    {
        public Desk Desk { get; }
        public ICollection<Card> LastCards { get; }
    }
}