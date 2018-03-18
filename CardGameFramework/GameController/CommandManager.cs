using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGameFramework.GameComponent;

namespace CardGameFramework.GameController
{
    public class CommandManager
    {
        private static List<ICommand> StandardCommands = new List<ICommand>();

        public static void Process(string text, Desk desk = null)
        {
            var split = text.Split(' ');
            var first = split.First();
            var others = split.Take(1).ToArray();
            var context = new CommandContext { Argc = others.Length, Argv = others, Desk = desk };

            foreach (var command in StandardCommands)
            {
                if (command.Matcher.IsMatch(first))
                {
                    command.Run(context);
                    return;
                }
            }

            //TODO Desk command
        }
    }
}