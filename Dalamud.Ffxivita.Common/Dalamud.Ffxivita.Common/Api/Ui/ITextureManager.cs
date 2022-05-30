using System;
using ImGuiScene;

namespace Dalamud.Ffxivita.Common.Api.Ui
{
    public interface ITextureManager : IDisposable
    {
        public TextureWrap? GetIconTexture(uint iconId);
    }
}
