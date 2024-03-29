﻿using System;
using Dalamud.Configuration;
using Dalamud.Ffxivita.Common.Api.Command;
using Dalamud.Ffxivita.Common.Api.Command.Attributes;
using Dalamud.Ffxivita.Common.Api.Config;
using Dalamud.Interface;

namespace Dalamud.Ffxivita.Common.Api.Ui.Window
{
    public abstract class ConfigWindow<TConfiguration> : Window, IConfigWindow<TConfiguration>, IDisposable,
        ICommandProvider where TConfiguration : class, IPluginConfiguration, new()
    {
        public TConfiguration Config => ConfigManager.Config;

        public void Save()
        {
            ConfigManager.Save();
        }

        public void Dispose()
        {
            UiBuilder.OpenConfigUi -= OnMainCommand;
            UiBuilder.Draw -= OnDraw;
        }

        [Command("")]
        [CommandHelp("Apre la finestra delle impostazioni per {Name}")]
        private void OnMainCommand()
        {
            this.Toggle();
        }

        private void OnDraw()
        {
            if (!IsDrawing)
            {
                return;
            }

            Draw();
        }

        internal void EnableHook()
        {
            UiBuilder.OpenConfigUi += OnMainCommand;
            UiBuilder.Draw += OnDraw;
        }
#pragma warning disable 8618
        internal IConfigManager<TConfiguration> ConfigManager { get; set; }
        internal UiBuilder UiBuilder { get; set; }
#pragma warning restore 8618
    }
}
