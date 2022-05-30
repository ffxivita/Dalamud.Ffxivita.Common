using System;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace Dalamud.Ffxivita.Common.Api.Chat
{
    public interface IChatClient : IDisposable
    {
        public void EnqueueChat(XivChatEntry entry);
        public void Print(SeString seString, string? sender = null, XivChatType? type = null);
        public void PrintError(SeString seString, string? sender = null, XivChatType? type = null);
    }
}
