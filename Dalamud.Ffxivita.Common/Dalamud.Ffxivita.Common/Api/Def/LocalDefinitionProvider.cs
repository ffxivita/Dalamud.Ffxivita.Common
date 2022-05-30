using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Dalamud.Ffxivita.Common.Api.Def
{
    internal sealed class LocalDefinitionProvider<TContainer> : DefinitionProvider<TContainer>
        where TContainer : DefinitionContainer, new()
    {
        private readonly string fallbackUrl;
        private readonly FileSystemWatcher watcher;

        public LocalDefinitionProvider(string filename, string fallbackUrl)
        {
            Filename = filename;
            this.fallbackUrl = fallbackUrl;

            if (!Directory.Exists(FfxivitaEnv.FfxivitaDir))
            {
                Directory.CreateDirectory(FfxivitaEnv.FfxivitaDir);
            }
            watcher = new FileSystemWatcher(FfxivitaEnv.FfxivitaDir, filename);

            watcher.Changed += OnDefinitionFileChanged;
            watcher.EnableRaisingEvents = true;
        }

        public override string Filename { get; }

        public override bool AllowObsoleteDefinitions => true;

        private void OnDefinitionFileChanged(object sender, FileSystemEventArgs e)
        {
            Task.Delay(1000, Cancellable.Token);
            Update(Cancellable.Token);
        }

        internal override JObject Fetch()
        {
            var localPath = Path.Combine(FfxivitaEnv.FfxivitaDir, Filename);
            if (!File.Exists(localPath))
            {
                using var remote = new RemoteDefinitionProvider<TContainer>(fallbackUrl, Filename);
                return remote.Fetch();
            }

            var content = File.ReadAllText(localPath);
            return JObject.Parse(content);
        }

        public override void Dispose()
        {
            base.Dispose();
            watcher.Dispose();
        }
    }
}
