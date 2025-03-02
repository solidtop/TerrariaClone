using System.Collections.Generic;
using System.Threading.Tasks;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class WorldSaver(string description, IChunkWriter chunkWriter, IChunkPathProvider chunkPathProvider) : WorldInitializerBase(description)
    {
        private readonly IChunkWriter _chunkWriter = chunkWriter;
        private readonly IChunkPathProvider _chunkPathProvider = chunkPathProvider;

        public override async Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            var tasks = new List<Task>();

            foreach (var chunk in state.Chunks)
            {
                var filePath = _chunkPathProvider.GetPath(chunk.Position);

                tasks.Add(Task.Run(() =>
                {
                    _chunkWriter.Write(chunk, filePath);
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
