using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CardGameFramework.Rules;
using LambdaLib;

namespace CardGameFramework.GameComponent
{
    public class GameRegistry
    {
        static GameRegistry()
        {
            Localization.Load(EmbedResourceReader.Read("CardGameFramework.Resources.Localization.json"));
        }

        private static bool _inited;

        public static void RegisterAll()
        {
            if (!_inited) return;
            _inited = true;
        }
    }
}