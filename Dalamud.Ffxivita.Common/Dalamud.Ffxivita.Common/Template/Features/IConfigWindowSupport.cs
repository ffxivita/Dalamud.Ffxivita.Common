using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api.Ui.Window;

namespace Dalamud.Ffxivita.Common.Template.Features
{
    public interface IConfigWindowSupport<TConfiguration> where TConfiguration : class, IPluginConfiguration, new()
    {
        public ConfigWindow<TConfiguration> CreateConfigWindow();
    }
}
