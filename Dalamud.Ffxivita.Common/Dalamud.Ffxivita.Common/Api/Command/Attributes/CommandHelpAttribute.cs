using System;
namespace Dalamud.Ffxivita.Common.Api.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandHelpAttribute : Attribute
    {
        public CommandHelpAttribute(string help)
        {
            Help = help;
        }

        public string Help { get; }
    }
}
