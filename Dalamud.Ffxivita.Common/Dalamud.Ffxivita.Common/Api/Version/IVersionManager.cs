namespace Dalamud.Ffxivita.Common.Api.Version
{
    public interface IVersionManager
    {
        public IGitVersion Plugin { get; }
        public IGitVersion Ffxivita { get; }
    }
}
