using System.Diagnostics;
using Dalamud.Ffxivita.Common.Api.Command.Attributes;

namespace Dalamud.Ffxivita.Common.Api.Command
{
    public class DirectoryCommands : ICommandProvider
    {
        [Command("xivita-data")]
        [CommandHelp("Apre la cartella AppData di FFXIVITA")]
        [HiddenCommand(HideInHelp = false)]
        private static void OnAppDataCommand()
        {
            Process.Start(FfxivitaEnv.FfxivitaDir);
        }

        [Command("xl-data")]
        [CommandHelp("Apre la cartella AppData di XIVLauncher")]
        [HiddenCommand(HideInHelp = false)]
        private static void OnXivLauncherAppDataCommand()
        {
            Process.Start(FfxivitaEnv.XivLauncherDir);
        }
    }
}
