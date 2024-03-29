﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dalamud.Ffxivita.Common.Api.Def
{
    public abstract class DefinitionProvider<TContainer> : IDefinitionProvider<TContainer>
        where TContainer : DefinitionContainer, new()
    {
        protected readonly CancellationTokenSource Cancellable = new();
        private readonly object containerLock = new();
        private readonly Task initializationTask;
        private TContainer? container;

        protected DefinitionProvider()
        {
            initializationTask = Task.Run(() => Update(Cancellable.Token), Cancellable.Token);
        }

        public abstract string Filename { get; }

        public TContainer Container
        {
            get
            {
                initializationTask.Wait();

                lock (containerLock)
                {
                    return container ?? throw new AggregateException($"Failed to fetch definition file. ({Filename})");
                }
            }
        }

        public virtual bool AllowObsoleteDefinitions => false;

        public void Update(CancellationToken token)
        {
            var json = Fetch();
            if (json == null)
            {
                return;
            }

            lock (containerLock)
            {
                container = json.ToObject<TContainer>(new JsonSerializer
                {
                    Converters =
                    {
                        new HexStringJsonConverter(),
                    },
                });

                var localGameVersion = ReadLocalGameVersion();
                if (localGameVersion != container?.Version)
                {
                    PluginLog.Warning(
                        "The game version \"{DefinitionGameVersion}\" is not supported yet. The local one is \"{LocalGameVersion}\".",
                        container?.Version ?? string.Empty,
                        localGameVersion);

                    if (!AllowObsoleteDefinitions)
                    {
                        container = new TContainer
                        {
                            Version = container?.Version,
                            Patch = container?.Patch,
                        };
                    }
                }
                else
                {
                    container.IsObsolete = false;

                    PluginLog.Information(
                        "The definition file for patch {GamePatch} \"{DefinitionFilename}\" was loaded. Local game version is \"{LocalGameVersion}\".",
                        container?.Patch ?? string.Empty,
                        Filename,
                        localGameVersion);
                }
            }

            PluginLog.Verbose("{DefinitionFilename}\n{DefinitionJson}",
                Filename,
                JsonConvert.SerializeObject(json, Formatting.Indented));
        }

        public virtual void Dispose()
        {
            Cancellable.Cancel();
        }

        internal abstract JObject? Fetch();

        private static string ReadLocalGameVersion()
        {
            // "C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game\ffxivgame.ver"
            var gameVersionPath = Path.Combine(FfxivitaEnv.GameDir, "ffxivgame.ver");

            return File.ReadAllText(gameVersionPath).Trim();
        }
    }
}
