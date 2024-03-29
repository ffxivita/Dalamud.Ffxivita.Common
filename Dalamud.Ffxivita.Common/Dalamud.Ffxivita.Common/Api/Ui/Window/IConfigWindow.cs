﻿using Dalamud.Configuration;

namespace Dalamud.Ffxivita.Common.Api.Ui.Window
{
    public interface IConfigWindow<out TConfiguration> : IWindow
        where TConfiguration : class, IPluginConfiguration, new()
    {
        public TConfiguration Config { get; }

        public void Save();
    }
}
