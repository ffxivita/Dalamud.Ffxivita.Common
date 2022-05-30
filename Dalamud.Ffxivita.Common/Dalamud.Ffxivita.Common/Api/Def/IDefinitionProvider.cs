using System;
using System.Threading;

namespace Dalamud.Ffxivita.Common.Api.Def
{
    public interface IDefinitionProvider<out TContainer> : IDisposable where TContainer : DefinitionContainer, new()
    {
        public string Filename { get; }
        public TContainer Container { get; }
        public bool AllowObsoleteDefinitions { get; }
        public void Update(CancellationToken token);
    }
}
