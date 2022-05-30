using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api;
using Dalamud.Ffxivita.Common.Api.Dalamud;
using Dalamud.Ffxivita.Common.Api.Def;
using Dalamud.Logging;
using Dalamud.Plugin;

namespace Dalamud.Ffxivita.Common.Template
{
    /// <summary>
    /// Fornisce la baase per i Plugin FFXIVITA.
    /// Si possono creare plugin compatibili con Dalamud ereditando questa base
    /// </summary>
    /// <typeparam name="TPlugin">Classe di Plugin</typeparam>
    /// <typeparam name="TConfiguration">Una classe di Configurazione che implementa Dalamud.Configuration.IPluginConfiguration</typeparam>
    /// <typeparam name="TDefinition">Definisce una classe esterna per i Plugin</typeparam>
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
    public abstract class
        FfxivitaPlugin<TPlugin, TConfiguration, TDefinition> : IFfxivitaPluginApi<TConfiguration, TDefinition>
        where TPlugin : FfxivitaPlugin<TPlugin, TConfiguration, TDefinition>
        where TConfiguration : class, IPluginConfiguration, new()
        where TDefinition : DefinitionContainer, new()
    {
        protected FfxivitaPlugin(DalamudPluginInterface pluginInterface)
        {
            Instance = this as TPlugin ?? throw new TypeAccessException("クラス インスタンスが型パラメータ: TPlugin と一致しません。");
            IsDisposed = false;
            Dalamud = new DalamudApi(pluginInterface);
            Ffxivita = new FfxivitaApi<TConfiguration, TDefinition>(Dalamud, Assembly, this);

            PluginLog.Information("プラグイン: {Name} の初期化に成功しました。バージョン = {Version}",
                Name,
                Ffxivita.Version.Plugin.InformationalVersion);
        }

        /// <summary>
        ///     プラグインのインスタンスの静的プロパティ。
        /// </summary>
#pragma warning disable 8618
        public static TPlugin Instance { get; private set; }
#pragma warning restore 8618

        public string Name => $"Ffxivita.{Instance.GetType().Name.Replace("Plugin", string.Empty)}";
        public bool IsDisposed { get; private set; }
        public TConfiguration Config => Ffxivita.Config.Config;
        public TDefinition? Definition => Ffxivita.Definition?.Container;
        public IDalamudApi Dalamud { get; }
        public IFfxivitaApi<TConfiguration, TDefinition> Ffxivita { get; }
        public Assembly Assembly => Instance.GetType().Assembly;

        #region IDisposable

        /// <summary>
        ///     Ffxivita プラグイン内で確保されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     .NET 管理リソースを解放します。
        /// </summary>
        protected virtual void ReleaseManaged()
        {
        }

        /// <summary>
        ///     .NET 管理外のリソースの解放を試みます。
        /// </summary>
        protected virtual void ReleaseUnmanaged()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;

            if (disposing)
            {
                ReleaseManaged();
                Ffxivita.Dispose();
            }

            ReleaseUnmanaged();
        }

        ~FfxivitaPlugin()
        {
            Dispose(false);
        }

        #endregion
    }
}
