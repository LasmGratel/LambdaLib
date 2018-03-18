using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.GameController
{
    public enum CommandPermission
    {
        Anyone,
        InGamePlayer,
        CurrentPlayer,
        Admin
    }

    public static class CommandHelper
    {
        public static string GetDescription(this ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}