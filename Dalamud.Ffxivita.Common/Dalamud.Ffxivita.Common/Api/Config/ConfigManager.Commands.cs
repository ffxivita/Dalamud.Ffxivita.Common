using System.Collections.Generic;
using System.Linq;
using Dalamud.Ffxivita.Common.Api.Chat;
using Dalamud.Ffxivita.Common.Api.Command;
using Dalamud.Ffxivita.Common.Api.Command.Attributes;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;

namespace Dalamud.Ffxivita.Common.Api.Config
{
   internal partial class ConfigManager<TConfiguration>
    {
        public class Commands : ICommandProvider
        {
            private readonly IChatClient chatClient;
            private readonly IConfigManager<TConfiguration> manager;
            private readonly ICommandProcessor processor;

            public Commands(IConfigManager<TConfiguration> manager, ICommandProcessor processor, IChatClient chatClient)
            {
                this.manager = manager;
                this.processor = processor;
                this.chatClient = chatClient;
            }

            [Command("config", "show")]
            [CommandHelp("Visualizza il valore corrente di {Name}")]
            [HiddenCommand(HideInHelp = false)]
            private void OnConfigShowCommand()
            {
                chatClient.Print(payloads =>
                {
                    payloads.Add(new TextPayload("Elenco Impostazioni:\n"));

                    foreach (var fieldInfo in EnumerateConfigFields())
                    {
                        var name = fieldInfo.Name;
                        var value = fieldInfo.GetValue(manager.Config);

                        payloads.Add(new TextPayload($"{processor.Prefix} config {name} {value}\n"));
                    }
                });
            }

            [Command("config")]
            [CommandHelp("Elenco impostazioni per {Name}")]
            [HiddenCommand(HideInHelp = false)]
            private void OnConfigListCommand()
            {
                var configKeys = EnumerateConfigFields().Select(x => x.Name);

                chatClient.Print(new List<Payload>
                {
                    new TextPayload($"Il nome della impostazione è {typeof(TConfiguration).FullName} Il nome del campo. Non case-sensitive\n"),
                    new TextPayload("Se il valore della impostazione è bool/string, tale valore può essere omesso.\n"),
                    new TextPayload("Se bool, viene automaticamente settato su true\n"),
                    new TextPayload("Se stringa, viene impostato come null\n"),
                    new TextPayload("Impostazioni Disponibili:\n"),
                    new TextPayload(string.Join("\n", configKeys)),
                });
            }

            [Command("config", "<key>", "<value?>")]
            [Command("configtts", "<key>", "<value?>")]
            [CommandHelp("{Name} の設定 <key> を <value?> に変更できます。")]
            [HiddenCommand(HideInHelp = false)]
            private void OnConfigUpdateCommand(CommandContext context)
            {
                var key = context.GetArgument("key");
                var value = context["value"];

                manager.TryUpdate(key, value, context.Command.Syntaxes[1] == "configtts");
            }
        }
    }
}
