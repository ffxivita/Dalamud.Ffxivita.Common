using System;
using Dalamud.Configuration;

namespace Dalamud.Ffxivita.Common.Api.Config
{
    public interface IConfigManager<out T> : IDisposable where T : IPluginConfiguration
    {
        public T Config { get; }

        public bool TryUpdate(string key, string? value, bool useTts);

        /// <summary>
        ///     Salva la configurazione del Plugin su File。
        ///     Viene chiamata in modo autonomo all'uscita dal plugin.
        /// </summary>
        public void Save();
    }
}
