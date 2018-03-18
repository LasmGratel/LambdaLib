using CardGameFramework.GameComponent;
using LambdaLib;

namespace CardGameFramework.GameController
{
    public interface ICommand
    {
        CommandName Name { get; }
        CommandPermission Permission { get; }
        IMatcher Matcher { get; }

        string Run(CommandContext context);
    }

    public class CommandContext
    {
        public Desk Desk { get; internal set; }
        public int Argc { get; internal set; }
        public string[] Argv { get; internal set; }
    }

    public interface IMatcher
    {
        bool IsMatch(string text);
    }
}