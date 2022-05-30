using System;
namespace Dalamud.Ffxivita.Common.Api.Def
{
    public interface IDefinitionManager<out TContainer> : IDisposable where TContainer : DefinitionContainer, new()
    {
        public IDefinitionProvider<TContainer> Provider { get; }

        public TContainer Container { get; }

        public bool TryUpdate(string key, string? value, bool useTts);
    }
}
