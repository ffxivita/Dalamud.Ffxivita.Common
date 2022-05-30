using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dalamud.Ffxivita.Common.Api.Command
{
    public interface ICommandProcessor : IDisposable
    {
        public string? Prefix { get; }
        public IReadOnlyList<FfxivitaCommand> Commands { get; }

        public bool ProcessCommand(string text);
        public void DispatchCommand(FfxivitaCommand command, Match match);
        public void RegisterCommandsByAttribute(ICommandProvider? instance);
    }
}
