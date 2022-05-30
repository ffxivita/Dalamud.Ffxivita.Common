using Dalamud.Ffxivita.Common.Api.Command;

namespace Dalamud.Ffxivita.Common.Template.Features
{
    public interface ICommandSupport : ICommandProvider
    {
        public string MainCommandPrefix { get; }
    }
}
