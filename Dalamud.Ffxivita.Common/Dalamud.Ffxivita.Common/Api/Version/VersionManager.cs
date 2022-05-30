namespace Dalamud.Ffxivita.Common.Api.Version
{
    internal partial class VersionManager : IVersionManager
    {
        public VersionManager(IGitVersion pluginVersion, IGitVersion libraryVersion)
        {
            Plugin = pluginVersion;
            Ffxivita = libraryVersion;
        }


        public IGitVersion Plugin { get; }
        public IGitVersion Ffxivita { get; }
    }
}
