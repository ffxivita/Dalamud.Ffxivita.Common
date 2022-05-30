using System;
using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api.Chat;
using Dalamud.Ffxivita.Common.Api.Command;
using Dalamud.Ffxivita.Common.Api.Config;
using Dalamud.Ffxivita.Common.Api.Def;
using Dalamud.Ffxivita.Common.Api.Input;
using Dalamud.Ffxivita.Common.Api.Network;
using Dalamud.Ffxivita.Common.Api.Ui;
using Dalamud.Ffxivita.Common.Api.Ui.Window;
using Dalamud.Ffxivita.Common.Api.Version;
using Dalamud.Ffxivita.Common.Api.Voiceroid2Proxy;
using Dalamud.Ffxivita.Common.Api.XivApi;

namespace Dalamud.Ffxivita.Common.Api
{
    public interface IFfxivitaApi<TConfiguration, out TDefinition> : IDisposable
        where TConfiguration : class, IPluginConfiguration, new() where TDefinition : DefinitionContainer, new()
    {
        public IChatClient Chat { get; }

        public ICommandProcessor? Command { get; }

        public ITextureManager Texture { get; }

        public IVersionManager Version { get; }

        public IVoiceroid2ProxyClient Voiceroid2Proxy { get; }

        public IXivApiClient XivApi { get; }

        public IKeyStrokeManager KeyStroke { get; }

        public INetworkInterceptor Network { get; }

        public IConfigManager<TConfiguration> Config { get; }

        public ConfigWindow<TConfiguration>? ConfigWindow { get; }

        public IDefinitionManager<TDefinition>? Definition { get; }
    }
}
