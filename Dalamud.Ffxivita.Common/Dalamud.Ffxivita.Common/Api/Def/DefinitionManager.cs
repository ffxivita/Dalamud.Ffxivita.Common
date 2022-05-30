using System;
using System.Collections.Generic;
using System.Reflection;
using Dalamud.Ffxivita.Common.Api.Chat;
using Dalamud.Ffxivita.Common.Api.Command;
using Dalamud.Ffxivita.Common.Api.Utils;
using Dalamud.Ffxivita.Common.Api.Voiceroid2Proxy;

namespace Dalamud.Ffxivita.Common.Api.Def
{
    internal partial class DefinitionManager<TContainer> : IDefinitionManager<TContainer>, ICommandProvider
        where TContainer : DefinitionContainer, new()
    {
        private readonly IChatClient chatClient;
        private readonly Func<IVoiceroid2ProxyClient> voiceroid2ProxyClient;

        public DefinitionManager(string url, IChatClient chatClient, Func<IVoiceroid2ProxyClient> voiceroid2ProxyClient)
        {
            Provider = DefinitionProviderFactory<TContainer>.Create(url);
            this.chatClient = chatClient;
            this.voiceroid2ProxyClient = voiceroid2ProxyClient;
        }

        public IDefinitionProvider<TContainer> Provider { get; }

        public bool TryUpdate(string key, string? value, bool useTts)
        {
            var updater = new FieldUpdater(Provider.Container, chatClient, voiceroid2ProxyClient.Invoke(), useTts);

            var fields = EnumerateDefinitionsFields();
            return updater.TryUpdate(key, value, fields);
        }

        public TContainer Container => Provider.Container;

        public void Dispose()
        {
            Provider.Dispose();
        }

        private IEnumerable<FieldInfo> EnumerateDefinitionsFields()
        {
            return Provider.Container.GetType().GetFields();
        }
    }
}
