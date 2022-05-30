using System;
using System.Diagnostics;
using System.IO;

namespace Dalamud.Ffxivita.Common.Api
{
    public class FfxivitaEnv
    {
        /// <summary>
        /// Directory di FFXIVITA
        /// </summary>
        public static string FfxivitaDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AHD", "FFXIVITA");

        /// <summary>
        /// Directory della cartella Cache
        /// </summary>
        public static string CacheDir => Path.Combine(FfxivitaDir, "cache");

        /// <summary>
        /// Cartella di Installazione di Final Fantasy XIV.
        /// Generalmente: "C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game"
        /// </summary>
        public static string GameDir => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule!.FileName)!;

        /// <summary>
        /// Cartella di XIVLauncher
        /// </summary>
        public static string XivLauncherDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncher");

        /// <summary>
        /// Cartella dove sono i file di configurazione dei Plugin
        /// </summary>
        public static string PluginConfDir => Path.Combine(XivLauncherDir, "pluginConfigs");
    }
}
