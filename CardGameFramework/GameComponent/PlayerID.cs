using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameComponent
{
    public struct PlayerID
    {
        public PlayerID(string id)
        {
            ID = id;
        }

        public string ID { get; }
    }
}