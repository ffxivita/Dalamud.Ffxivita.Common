using System;
using System.Reflection;
using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api.Dalamud;
using Dalamud.Ffxivita.Common.Api;
using Dalamud.Ffxivita.Common.Api.Def;

namespace Dalamud.Ffxivita.Common.Api
{
    /// <summary>
    /// Interfacce per le varie API Implementate dai Plugin FFXIVITA
    /// </summary>
    public interface IFfxivitaPluginApi<TConfiguration, out TDefinition> : IDisposable
        where TConfiguration : class, IPluginConfiguration, new() where TDefinition : DefinitionContainer, new()
    {
        /// <summary>
        /// Ottiene il nome del Plugin che sarà notificato a Dalamud.
        /// Implementato da Dalamud.Plugin.IDalamudPlugin.
        /// </summary>
        public string Name { get; }

        public bool IsDisposed { get; }

        /// <summary>
        /// Una istanza del costruttore implementato da Dalamud.Configuration.IPluginConfiguration.
        /// </summary>
        public TConfiguration Config { get; }

        public TDefinition? Definition { get; }

        public IDalamudApi Dalamud { get; }

        public IFfxivitaApi<TConfiguration, TDefinition> Ffxivita { get; }

        /// <summary>
        /// Ottiene Assembly  contenente il codice del Plugin.
        /// </summary>
        public Assembly Assembly { get; }
    }
}
