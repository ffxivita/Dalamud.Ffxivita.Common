using Dalamud.Configuration;
using Dalamud.Plugin;

namespace Dalamud.Ffxivita.Common.Template
{
    public abstract class
        FfxivitaPlugin<TPlugin, TConfiguration> : FfxivitaPlugin<TPlugin, TConfiguration, EmptyDefinitionContainer>
        where TPlugin : FfxivitaPlugin<TPlugin, TConfiguration, EmptyDefinitionContainer>
        where TConfiguration : class, IPluginConfiguration, new()
    {
        protected FfxivitaPlugin(DalamudPluginInterface pluginInterface) : base(pluginInterface)
        {
        }
    }
}
