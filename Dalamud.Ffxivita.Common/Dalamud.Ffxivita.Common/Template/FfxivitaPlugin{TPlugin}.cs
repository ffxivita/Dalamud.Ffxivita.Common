using Dalamud.Plugin;
namespace Dalamud.Ffxivita.Common.Template
{
    public abstract class FfxivitaPlugin<TPlugin> : FfxivitaPlugin<TPlugin, EmptyConfig, EmptyDefinitionContainer>
        where TPlugin : FfxivitaPlugin<TPlugin, EmptyConfig, EmptyDefinitionContainer>
    {
        protected FfxivitaPlugin(DalamudPluginInterface pluginInterface) : base(pluginInterface)
        {
        }
    }
}
