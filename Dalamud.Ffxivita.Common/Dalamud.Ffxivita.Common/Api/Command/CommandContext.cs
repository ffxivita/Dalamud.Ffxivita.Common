using System.Text.RegularExpressions;

namespace Dalamud.Ffxivita.Common.Api.Command
{
    public sealed class CommandContext
    {
        /// <summary>
        /// Inizializza il contesto del comando.
        /// </summary>
        /// <param name="command">Comando</param>
        /// <param name="match">Risultato dalle Espressioni Regolari</param>
        internal CommandContext(FfxivitaCommand command, Match match)
        {
            Command = command;
            Match = match;
        }

        /// <summary>
        /// Richiama il comando
        /// </summary>
        public FfxivitaCommand Command { get; }

        /// <summary>
        /// Argomenti forniti dal Comando
        /// </summary>
        public Match Match { get; }

        public string? this[string name] => Match.Groups.TryGetValue(name, out var result) ? result.Value : null;

        public string GetArgument(string name)
        {
            return Match.Groups[name].Value;
        }
    }
}
