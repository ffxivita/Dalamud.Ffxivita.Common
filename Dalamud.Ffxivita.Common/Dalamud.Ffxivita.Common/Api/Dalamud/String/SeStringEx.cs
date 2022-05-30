using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace Dalamud.Ffxivita.Common.Api.Dalamud.String
{
    public static class SeStringEx
    {
        public static string ToUtf8String(this SeString seString)
        {
            return string.Join(string.Empty, seString.Payloads.Select(x => x.ToUtf8String()));
        }

        public static string ToUtf8String(this Game.Text.SeStringHandling.Payload payload)
        {
            return Encoding.UTF8.GetString(payload.Encode());
        }

        public static string ToUtf8String(this IEnumerable<Game.Text.SeStringHandling.Payload> payloads)
        {
            return string.Join(string.Empty, payloads.Select(x => x.ToUtf8String()));
        }

        public static string ToUtf8String(this Game.Text.SeStringHandling.Payload[] payloads)
        {
            return string.Join(string.Empty, payloads.Select(x => x.ToUtf8String()));
        }
    }
}
