using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public class Player
    {
        public Player(PlayerID id)
        {
            ID = id;
        }

        public PlayerID ID { get; }
    }
}