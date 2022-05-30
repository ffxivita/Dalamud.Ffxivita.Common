using System.IO;

namespace Dalamud.Ffxivita.Common.Api.Def
{
    public static class DefinitionProviderFactory<TContainer> where TContainer : DefinitionContainer, new()
    {
        public static IDefinitionProvider<TContainer> Create(string url)
        {
            var filename = Path.GetFileName(url);

#if DEBUG
            return new LocalDefinitionProvider<TContainer>(filename, url);
#else
            return new RemoteDefinitionProvider<TContainer>(url, filename);
#endif
        }
    }
}
