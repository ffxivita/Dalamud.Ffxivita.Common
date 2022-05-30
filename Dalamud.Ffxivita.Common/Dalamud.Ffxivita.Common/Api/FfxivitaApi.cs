using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api.Chat;
using Dalamud.Ffxivita.Common.Api.Command;
using Dalamud.Ffxivita.Common.Api.Config;
using Dalamud.Ffxivita.Common.Api.Dalamud;
using Dalamud.Ffxivita.Common.Api.Def;
using Dalamud.Ffxivita.Common.Api.Input;
using Dalamud.Ffxivita.Common.Api.Network;
using Dalamud.Ffxivita.Common.Api.Ui;
using Dalamud.Ffxivita.Common.Api.Ui.Window;
using Dalamud.Ffxivita.Common.Api.Version;
using Dalamud.Ffxivita.Common.Api.Voiceroid2Proxy;
using Dalamud.Ffxivita.Common.Api.XivApi;
using Dalamud.Ffxivita.Common.Template.Features;

namespace Dalamud.Ffxivita.Common.Api
{
    internal sealed class FfxivitaApi<TConfiguration, TDefinition> : IFfxivitaApi<TConfiguration, TDefinition>
        where TConfiguration : class, IPluginConfiguration, new() where TDefinition : DefinitionContainer, new()
    {
        public FfxivitaApi(IDalamudApi api,
            Assembly assembly,
            IFfxivitaPluginApi<TConfiguration, TDefinition> plugin)
        {
            Dalamud = api;
            Assembly = assembly;
            Plugin = plugin;

            Command?.RegisterCommandsByAttribute(new VersionManager.Commands(Version, Chat));
            if (Definition != null)
            {
                Command?.RegisterCommandsByAttribute(new DefinitionManager<TDefinition>.Commands(Definition));
            }
            Command?.RegisterCommandsByAttribute(new ConfigManager<TConfiguration>.Commands(Config, Command, Chat));
            Command?.RegisterCommandsByAttribute(ConfigWindow);
        }

        private IDalamudApi Dalamud { get; }
        private Assembly Assembly { get; }
        private IFfxivitaPluginApi<TConfiguration, TDefinition> Plugin { get; }

        public IChatClient Chat =>
            ServiceContainer.GetOrPut(() => new ChatClient(Plugin.Name, Dalamud.ChatGui, Dalamud.ClientState));

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public ICommandProcessor? Command => ServiceContainer.GetOrPutOptional(() =>
        {
            var processor = Plugin switch
            {
                ICommandSupport commandSupport => new CommandProcessor(Plugin.Name,
                    commandSupport.MainCommandPrefix,
                    Dalamud.ChatGui,
                    Chat,
                    Dalamud.CommandManager),
                ICommandProvider => new CommandProcessor(Plugin.Name,
                    null,
                    Dalamud.ChatGui,
                    Chat,
                    Dalamud.CommandManager),
                _ => null,
            };

            if (processor == null)
            {
                return null;
            }

            processor.RegisterCommandsByAttribute(new DirectoryCommands());
            processor.RegisterCommandsByAttribute((ICommandProvider) Plugin);
            return processor;
        });

        public IConfigManager<TConfiguration> Config => ServiceContainer.GetOrPut(() =>
            new ConfigManager<TConfiguration>(Chat, () => Voiceroid2Proxy, Plugin.Name));
        public ConfigWindow<TConfiguration>? ConfigWindow => ServiceContainer.GetOrPutOptional(() =>
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (Plugin is IConfigWindowSupport<TConfiguration> configWindowSupport)
            {
                var window = configWindowSupport.CreateConfigWindow();
                window.ConfigManager = Config;
                window.UiBuilder = Dalamud.PluginInterface.UiBuilder;
                window.EnableHook();

                return window;
            }

            return null;
        });

        public IDefinitionManager<TDefinition>? Definition => ServiceContainer.GetOrPutOptional(() =>
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (Plugin is IDefinitionSupport definitionSupport)
            {
                return new DefinitionManager<TDefinition>(definitionSupport.DefinitionUrl, Chat, () => Voiceroid2Proxy);
            }

            return null;
        });

        public ITextureManager Texture => ServiceContainer.GetOrPut(() =>
            new TextureManager(Dalamud.DataManager, Dalamud.PluginInterface.UiBuilder));
        public IVersionManager Version => ServiceContainer.GetOrPut(() => new VersionManager(
            new GitVersion(Assembly),
            new GitVersion(Assembly.GetExecutingAssembly())));
        public IVoiceroid2ProxyClient Voiceroid2Proxy => ServiceContainer.GetOrPut(() => new Voiceroid2ProxyClient());
        public IXivApiClient XivApi => ServiceContainer.GetOrPut(() => new XivApiClient());
        public IKeyStrokeManager KeyStroke => ServiceContainer.GetOrPut(() => new KeyStrokeManager());
        public INetworkInterceptor Network =>
            ServiceContainer.GetOrPut(() => new NetworkInterceptor(Dalamud.GameNetwork));

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServiceContainer.DestroyAll();
            }
        }

        ~FfxivitaApi()
        {
            Dispose(false);
        }

        #endregion
    }
}
