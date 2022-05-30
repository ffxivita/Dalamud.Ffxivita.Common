using Newtonsoft.Json;
namespace Dalamud.Ffxivita.Common.Api.Def
{
    public abstract class DefinitionContainer
    {
        public string? Patch;

        public string? Version;
        [JsonIgnore]
        public bool IsObsolete { get; internal set; } = true;
    }
}
